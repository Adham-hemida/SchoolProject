using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Interfaces.IAuthentication;
using SchoolProject.Infrastructure.Implementation.Authentication;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Role;
using SchoolProject.Application.Abstractions.Consts;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(DefaultRoles.Admin.Name)]
public class RolesController(IRoleService roleService) : ControllerBase
{
	private readonly IRoleService _roleService = roleService;

	[HttpGet("")]
	public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellationToken)
	{
		var roles = await _roleService.GetAllAsync(includeDisabled, cancellationToken);
		return Ok(roles);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get([FromRoute] string id)
	{
		var result = await _roleService.GetAsync(id);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("")]
	public async Task<IActionResult> Add([FromBody] RoleRequest request)
	{
		var result = await _roleService.AddAsync(request);
		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request)
	{
		var result = await _roleService.UpdateAsync(id, request);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/toggle-status")]
	public async Task<IActionResult> ToggleStatus([FromRoute] string id)
	{
		var result = await _roleService.ToggleSatausAsync(id);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}


}
