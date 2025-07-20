using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IAssignmentService
{
	Task<Result<AssignmentIdResponse>> AddAsync(Guid teacherId, AssignmentRequest request, CancellationToken cancellationToken);
	Task<Result<AssignmentResponse>> GetByIdAsync(Guid assignmentId, CancellationToken cancellationToken);
	Task<Result<List<AssignmentSubmissionResponse>>> GetAssignmentSubmissionsAsync(Guid assignmentId, CancellationToken cancellationToken);
	Task<Result> UpdateAsync(Guid assignmentId, int subjectId, AssignmentUpdateRequest request, CancellationToken cancellationToken);
}
