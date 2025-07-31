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

	[HttpPost("teacher/{teacherId}/assign-user")]
	public async Task<IActionResult> AssignUserToTeacher([FromRoute] Guid teacherId, [FromBody] CreateUserRequest request, CancellationToken cancellationToken)
	{
		var result = await _userService.AssignUserToTeacherAsync(request, teacherId, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("")]
	public async Task<IActionResult> create([FromBody] CreateUserWithRolesRequest request, CancellationToken cancellationToken)
	{
		var result = await _userService.CreateAsync(request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{userId}")]
	public async Task<IActionResult> Update([FromRoute] string userId, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
	{
		var result = await _userService.UpdateAsync(userId, request, cancellationToken);
		return result.IsSuccess ? NoContent()
			: result.ToProblem();
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> Get([FromRoute] string userId)
	{
		var result = await _userService.GetAsync(userId);
		return result.IsSuccess ? Ok(result.Value)
			: result.ToProblem();
	}

	[HttpPut("{userId}/unlock")]
	public async Task<IActionResult> Unlock([FromRoute] string userId)
	{
		var result = await _userService.UnlockAsync(userId);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

}
