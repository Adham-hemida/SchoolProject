using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.User;
using SchoolProject.Infrastructure.Implementation.Authentication;
namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IUserService
{
	Task<Result<StudentUserResponse>> CreateStudentWithUserAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);
	Task<Result<StudentUserResponse>> AssignUserToStudentAsync(CreateUserRequest request, Guid studentId, CancellationToken cancellationToken = default);
	Task<Result<TeacherUserResponse>> CreateTeacherWithUserAsync(TeacherUserRequest request, CancellationToken cancellationToken = default);
	Task<Result<TeacherUserResponse>> AssignUserToTeacherAsync(CreateUserRequest request, Guid teacherId, CancellationToken cancellationToken = default);
	Task<Result<UserResponse>> CreateAsync(CreateUserWithRolesRequest request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default);
	Task<Result<UserResponse>> GetAsync(string userId);
	Task<Result> UnlockAsync(string userId);
}
