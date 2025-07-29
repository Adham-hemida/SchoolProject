using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DepartmentSubjectsController(IDepartmentSubjectService departmentSubjectService) : ControllerBase
{
	private readonly IDepartmentSubjectService _departmentSubjectService = departmentSubjectService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetAllSubjectsOfDepartment([FromRoute] int id, CancellationToken cancellationToken = default)
	{
		var result = await _departmentSubjectService.GetAllSubjectsOfDepartmentAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
	
	[HttpGet("")]
	public async Task<IActionResult> GetAllSubjectsOfAllDepartments( CancellationToken cancellationToken = default)
	{
		var result = await _departmentSubjectService.GetAllSubjectsOfAllDepartmentsAsync( cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
