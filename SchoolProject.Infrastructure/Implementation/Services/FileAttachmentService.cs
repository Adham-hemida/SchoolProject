using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.FileAttachment;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class FileAttachmentService(IWebHostEnvironment webHostEnvironment,IUnitOfWork unitOfWork) : IFileAttachmentService
{
	private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/AssignmentFiles";
	private readonly string _fileSubmissionPath = $"{webHostEnvironment.WebRootPath}/StudentSubmissions";
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<Guid>> UploadAssignmentFileAsync(Guid assignmentId, UploadFileRequest file, CancellationToken cancellationToken)
	{
		var assignment = await _unitOfWork.Repository<Assignment>().FindAsync(x => x.Id == assignmentId, null, cancellationToken);

		if (assignment is null)
			return Result.Failure<Guid>(AssignmentErrors.AssignmentNotFound);

		if (file.File is null || file.File.Length == 0)
			return Result.Failure<Guid>(FileAttachmentErrors.FileAttachmentNotFound);

		var randomFileName = Path.GetRandomFileName();

		var uploadedFile = new FileAttachment
		{
			FileName = file.File.FileName,  
			StoredFileName = randomFileName,
			ContentType = file.File.ContentType,
			FileExtension = Path.GetExtension(file.File.FileName) ,
			AssignmentId = assignmentId
		};

		var path = Path.Combine(_filePath, randomFileName);

		using var stream = File.Create(path);
		await file.File.CopyToAsync(stream, cancellationToken);

		await _unitOfWork.Repository<FileAttachment>().CreateAsync(uploadedFile, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(uploadedFile.Id);
	}

	public async Task<Result> UploadStudentSubmissionAsync(Guid assignmentId, Guid studentId, UploadFileRequest file, CancellationToken cancellationToken)
	{
		var assignment = await _unitOfWork.Repository<Assignment>().FindAsync(x => x.Id == assignmentId, null, cancellationToken);
		
		if (assignment is null)
			return Result.Failure(AssignmentErrors.AssignmentNotFound);
		
		var alreadySubmitte = await _unitOfWork.Repository<StudentSubmission>()
			.AnyAsync(x => x.AssignmentId == assignmentId && x.StudentId == studentId, cancellationToken);

		if (alreadySubmitte)
			return Result.Failure(StudentSubmissionErrors.StudentAlreadySubmitted);

		if (file.File is null || file.File.Length == 0)
			return Result.Failure(FileAttachmentErrors.FileAttachmentNotFound);
		

		var randomFileName = Path.GetRandomFileName();
		var uploadedFile = new FileAttachment
		{
			FileName = file.File.FileName,
			StoredFileName = randomFileName,
			ContentType = file.File.ContentType,
			FileExtension = Path.GetExtension(file.File.FileName),
			AssignmentId = assignmentId
		};
		var path = Path.Combine(_fileSubmissionPath, randomFileName);

		using var stream = File.Create(path);
		await file.File.CopyToAsync(stream, cancellationToken);
		await _unitOfWork.Repository<FileAttachment>().CreateAsync(uploadedFile, cancellationToken);

		var studentSubmission = new StudentSubmission
		{
			AssignmentId = assignmentId,
			StudentId = studentId,
			FileAttachmentId = uploadedFile.Id,
			SubmittedAt = DateTime.UtcNow
		};
		await _unitOfWork.Repository<StudentSubmission>().CreateAsync(studentSubmission, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();

	}


	public async Task<Result<(byte[] fileContent, string contentType, string fileName)>> DownloadAssignmentFileAsync(Guid fileId, Guid assignmentId,int subjectId, CancellationToken cancellationToken = default)
	{
		var subject = await _unitOfWork.Repository<Subject>()
			.FindAsync(x => x.Id == subjectId, null, cancellationToken);

		if (subject == null)
			return Result.Failure<(byte[] fileContent, string contentType, string fileName)>(SubjectErrors.SubjectNotFound);

		var assignment = await _unitOfWork.Repository<Assignment>()
			.FindAsync(x => x.Id == assignmentId && x.SubjectId==subjectId , null, cancellationToken);

		if (assignment == null)
			return Result.Failure<(byte[] fileContent, string contentType, string fileName)>(AssignmentErrors.AssignmentNotFound);

		var file = await _unitOfWork.Repository<FileAttachment>()
			.FindAsync(x => x.Id == fileId && x.AssignmentId==assignmentId, null, cancellationToken);

		if (file == null)
			return Result.Failure<(byte[] fileContent, string contentType, string fileName)>(FileAttachmentErrors.FileAttachmentNotFound);

		var path = Path.Combine(_filePath, file.StoredFileName);
		MemoryStream memoryStream = new();
		using FileStream fileStream = new(path, FileMode.Open);
		fileStream.CopyTo(memoryStream);
		memoryStream.Position = 0; // Reset the position to the beginning of the stream  
		return Result.Success((memoryStream.ToArray(), file.ContentType, file.FileName));
	}

	public async Task<Result<(byte[] fileContent, string contentType, string fileName)>> DownloadSubmissionsFileAsync(Guid fileId, CancellationToken cancellationToken = default)
	{

		var file = await _unitOfWork.Repository<FileAttachment>()
			.FindAsync(x => x.Id == fileId  , null, cancellationToken);

		if (file == null)
			return Result.Failure<(byte[] fileContent, string contentType, string fileName)>(FileAttachmentErrors.FileAttachmentNotFound);

		// التحقق أن الملف مرتبط بتسليم طالب فعليًا
		var submissionExists = await _unitOfWork.Repository<StudentSubmission>()
			.AnyAsync(x => x.FileAttachmentId == fileId, cancellationToken);

		if (!submissionExists)
			return Result.Failure<(byte[] fileContent, string contentType, string fileName)>(StudentSubmissionErrors.SubmissionNotFound);


		var path = Path.Combine(_fileSubmissionPath, file.StoredFileName);
		MemoryStream memoryStream = new();
		using FileStream fileStream = new(path, FileMode.Open);
		fileStream.CopyTo(memoryStream);
		memoryStream.Position = 0; // Reset the position to the beginning of the stream  
		return Result.Success((memoryStream.ToArray(), file.ContentType, file.FileName));
	}


}
