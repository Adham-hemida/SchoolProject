using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IStudentService
{
	Task<Result<StudentResponse>>GetAsync(int DepartmentId, Guid Id, CancellationToken cancellationToken = default);
	Task<Result<IEnumerable<StudentBasicResponse>>>GetAllAsync(int DepartmentId, CancellationToken cancellationToken = default);
	Task<Result<StudentBasicResponse>>AddAsync(int DepartmentId, StudentRequest request, CancellationToken cancellationToken = default);
}
