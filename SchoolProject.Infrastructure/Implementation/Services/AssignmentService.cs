using Mapster;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Assignment;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class AssignmentService(IUnitOfWork unitOfWork) : IAssignmentService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<AssignmentIdResponse>> AddAsync(Guid teacherId, AssignmentRequest request, CancellationToken cancellationToken)
	{
		var subjectIsExist = await _unitOfWork.Repository<Subject>()
			.AnyAsync(x => x.Id == request.SubjectId && x.TeacherId == teacherId, cancellationToken);

		if (!subjectIsExist)
			return Result.Failure<AssignmentIdResponse>(SubjectErrors.SubjectNotFound);

		var assignmentIsExist = await _unitOfWork.Repository<Assignment>()
			.AnyAsync(x => x.Title.Trim() == request.Title.Trim() && x.SubjectId == request.SubjectId, cancellationToken);

		if(assignmentIsExist)
			return Result.Failure<AssignmentIdResponse>(AssignmentErrors.DuplicatedAssignment);

		var assignment = new Assignment
		{
			Title = request.Title.Trim(),
			SubjectId = request.SubjectId,
			IsActive=true
		};

	
		await _unitOfWork.Repository<Assignment>().CreateAsync(assignment, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(assignment.Adapt<AssignmentIdResponse>());
	}
}
