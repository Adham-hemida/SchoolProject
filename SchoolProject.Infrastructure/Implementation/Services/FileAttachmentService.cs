using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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



	

}
