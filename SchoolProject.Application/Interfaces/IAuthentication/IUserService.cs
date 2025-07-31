using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.User;
using SchoolProject.Infrastructure.Implementation.Authentication;
namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IUserService
{
	Task<Result<StudentUserResponse>> CreateStudentUserAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);
}
