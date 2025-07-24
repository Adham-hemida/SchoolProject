using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.User;
using SchoolProject.Application.Extensions;
using SchoolProject.Application.Interfaces.IAuthentication;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountService accountService) : ControllerBase
{
	private readonly IAccountService _accountService = accountService;

	[HttpGet("")]
	public async Task<IActionResult> ProfileInfo(CancellationToken cancellationToken)
	{
		var result = await _accountService.GetProfileInfoAsync(User.GetUserId()!, cancellationToken);
		return Ok(result.Value);
	}

	[HttpPut("change-password")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
	{
		var result = await _accountService.ChangePasswordAsync(User.GetUserId()!, request);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
