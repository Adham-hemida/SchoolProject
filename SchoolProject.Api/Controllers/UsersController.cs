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

	[HttpPost("")]
	public async Task<IActionResult> Create( [FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
	{
		var result = await _userService.CreateStudentUserAsync( request, cancellationToken);
		return result.IsSuccess ?Ok( result.Value ) : result.ToProblem();
	}
}
