using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Infrastructure.Implementation.Services;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DepartmentsController(IDepartmentService departmentService) : ControllerBase
{
	private readonly IDepartmentService _departmentService = departmentService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
	{
		var result = await _departmentService.GetByIdAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
