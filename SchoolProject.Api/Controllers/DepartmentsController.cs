using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Infrastructure.Implementation.Services;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Contracts.Department;
using Microsoft.AspNetCore.Authorization;
using SchoolProject.Application.Abstractions.Consts;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DepartmentsController(IDepartmentService departmentService) : ControllerBase
{
	private readonly IDepartmentService _departmentService = departmentService;

	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Teacher.Name},{DefaultRoles.Student.Name}")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
	{
		var result = await _departmentService.GetByIdAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Teacher.Name},{DefaultRoles.Student.Name}")]
	[HttpGet("")]
	public async Task<IActionResult> GetAll( CancellationToken cancellationToken = default)
	{
		var result = await _departmentService.GetAllAsync( cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPost("")]
	public async Task<IActionResult> Create( [FromBody] DepartmentRequest request, CancellationToken cancellationToken)
	{
		var result = await _departmentService.AddAsync( request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(GetById), new {  id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] int  id, [FromBody] DepartmentRequest request, CancellationToken cancellationToken)
	{
		var result = await _departmentService.UpdateAsync( id, request, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}


	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPut("{id}/toggleStatus")]
	public async Task<IActionResult> ToggleStatus([FromRoute] int  id, CancellationToken cancellationToken)
	{
		var result = await _departmentService.ToggleStatusAsync( id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
