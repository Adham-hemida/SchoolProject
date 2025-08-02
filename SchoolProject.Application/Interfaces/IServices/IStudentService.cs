using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Common;
using SchoolProject.Application.Contracts.Student;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IStudentService
{
	Task<Result<StudentResponse>>GetAsync(int departmentId, Guid Id, CancellationToken cancellationToken = default);
	Task<Result<PaginatedList<StudentBasicResponse>>> GetAllAsync(int departmentId, RequestFilters filters, CancellationToken cancellationToken = default);
	Task<Result<StudentBasicResponse>>AddAsync(int departmentId, StudentRequest request, CancellationToken cancellationToken = default);
	Task<Result>UpdateAsync(int departmentId, Guid id, UpdateStudentRequest request, CancellationToken cancellationToken = default);
	Task<Result> AssignStudentToDepartmentAsync(int departmentId, Guid id, CancellationToken cancellationToken = default);
	Task<Result>ToggleStatusAsync(int departmentId, Guid id, CancellationToken cancellationToken = default);
}
