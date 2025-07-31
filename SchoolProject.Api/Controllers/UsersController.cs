using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Contracts.User;
using SchoolProject.Application.Interfaces.IAuthentication;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
	private readonly IUserService _userService = userService;

	[HttpPost("student/create-with-user")]
	public async Task<IActionResult> CreateStudentWithUser( [FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
	{
		var result = await _userService.CreateStudentWithUserAsync( request, cancellationToken);
		return result.IsSuccess ?Ok( result.Value ) : result.ToProblem();
	}
	
	[HttpPost("student/{studentId}/assign-user")]
	public async Task<IActionResult> AssignUserToStudent([FromRoute] Guid studentId, [FromBody] CreateUserRequest request,  CancellationToken cancellationToken)
	{
		var result = await _userService.AssignUserToStudentAsync( request, studentId, cancellationToken);
		return result.IsSuccess ?Ok( result.Value ) : result.ToProblem();
	}

	[HttpPost("teacher/create-with-user")]
	public async Task<IActionResult> CreateTeacherWithUser([FromBody] TeacherUserRequest request, CancellationToken cancellationToken)
	{
		var result = await _userService.CreateTeacherWithUserAsync(request, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
