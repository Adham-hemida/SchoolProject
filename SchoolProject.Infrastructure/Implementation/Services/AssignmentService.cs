using Mapster;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Assignment;
using SchoolProject.Application.Contracts.FileAttachment;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class AssignmentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IAssignmentService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public async Task<Result<AssignmentIdResponse>> AddAsync(Guid teacherId, AssignmentRequest request, CancellationToken cancellationToken)
	{
		var subjectIsExist = await _unitOfWork.Repository<Subject>()
			.AnyAsync(x => x.Id == request.SubjectId && x.TeacherId == teacherId, cancellationToken);

		if (!subjectIsExist)
			return Result.Failure<AssignmentIdResponse>(SubjectErrors.SubjectNotFound);

		var assignmentIsExist = await _unitOfWork.Repository<Assignment>()
			.AnyAsync(x => x.Title.Trim() == request.Title.Trim() && x.SubjectId == request.SubjectId, cancellationToken);

		if (assignmentIsExist)
			return Result.Failure<AssignmentIdResponse>(AssignmentErrors.DuplicatedAssignment);

		var assignment = new Assignment
		{
			Title = request.Title.Trim(),
			SubjectId = request.SubjectId,
			IsActive = true
		};


		await _unitOfWork.Repository<Assignment>().CreateAsync(assignment, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(assignment.Adapt<AssignmentIdResponse>());
	}

	public async Task<Result<AssignmentResponse>> GetByIdAsync(Guid assignmentId, CancellationToken cancellationToken)
	{
		var request = _httpContextAccessor.HttpContext?.Request;
		var origin = $"{request?.Scheme}://{request?.Host}";

		var assignment = await _unitOfWork.Repository<Assignment>()
			.GetAsQueryable()
			.Where(x => x.Id == assignmentId)
			.Include(x => x.Subject)
			.Include(x => x.FileAttachments)
			.FirstOrDefaultAsync(cancellationToken);

		if (assignment is null)
			return Result.Failure<AssignmentResponse>(AssignmentErrors.AssignmentNotFound);

		var response = new AssignmentResponse
			(
			 assignment.Id,
			  assignment.Title,
		assignment.Subject.Name,
			  assignment.FileAttachments.Select(x => new FileAttachmentResponse(x.Id, $"{origin}/api/FileAttachments/assignment/{x.AssignmentId}/subject/{assignment.SubjectId}/download/{x.Id}")).ToList()
			);

		return Result.Success(response);
	}

	public async Task<Result<List<AssignmentSubmissionResponse>>> GetAssignmentSubmissionsAsync(Guid assignmentId, CancellationToken cancellationToken)
	{
		var request = _httpContextAccessor.HttpContext?.Request;
		var origin = $"{request?.Scheme}://{request?.Host}";


		var assignment = await _unitOfWork.Repository<Assignment>()
				.FindAsync(x => x.Id == assignmentId, null, cancellationToken);

		if (assignment == null)
			return Result.Failure<List<AssignmentSubmissionResponse>>(AssignmentErrors.AssignmentNotFound);

		var files = await _unitOfWork.Repository<FileAttachment>()
            .GetAsQueryable()
            .Where(x => x.AssignmentId == assignmentId)
			.Include(x => x.Assignment)
			.Include(ss => ss.StudentSubmissions)
			   .ThenInclude(s=>s.Student)
			.Select(x => new AssignmentSubmissionResponse(
	x.Id,
	$"{origin}/api/FileAttachments/download/{x.Id}"
             ))
			.ToListAsync(cancellationToken);

		

		return Result.Success(files);
	}

}
