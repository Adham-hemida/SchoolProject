using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Infrastructure.Implementation.Services;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Contracts.Teacher;
using Microsoft.AspNetCore.Authorization;
using SchoolProject.Application.Contracts.Common;
using SchoolProject.Application.Abstractions.Consts;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(DefaultRoles.Admin.Name)]
public class TeachersController(ITeacherService teacherService) : ControllerBase
{
	private readonly ITeacherService _teacherService = teacherService;

	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Teacher.Name}")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var result = await _teacherService.GetByIdAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Teacher.Name},{DefaultRoles.Student.Name}")]
	[HttpGet("")]
	public async Task<IActionResult> GetAll([FromQuery] RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var result = await _teacherService.GetAllAsync(filters,cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}


	[HttpPost("")]
	public async Task<IActionResult> Create([FromBody] TeacherRequest request, CancellationToken cancellationToken)
	{
		var result = await _teacherService.AddAsync(request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TeacherRequest request, CancellationToken cancellationToken)
	{
		var result = await _teacherService.UpdateAsync(id, request, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/toggleStatus")]
	public async Task<IActionResult> ToggleStatus([FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var result = await _teacherService.ToggleStatusAsync(id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Teacher.Name}")]
	[HttpPost("teachers/{teacherId}/assign-subject/{id}")]
	public async Task<IActionResult> AddSubjectToTeacher([FromRoute] int id, [FromRoute] Guid teacherId, CancellationToken cancellationToken)
	{
		var result = await _teacherService.AddSubjectToTeacherAsync(id, teacherId, cancellationToken);
		return result.IsSuccess ? Ok(id) : result.ToProblem();
	}
}
