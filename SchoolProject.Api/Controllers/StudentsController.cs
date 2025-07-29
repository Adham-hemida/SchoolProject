using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Interfaces.IServices;
namespace SchoolProject.Api.Controllers;
[Route("api/Department/{DepartmentId}[controller]")]
[ApiController]
[Authorize]
public class StudentsController(IStudentService studentService) : ControllerBase
{
	private readonly IStudentService _studentService = studentService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var result = await _studentService.GetAsync(DepartmentId, id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("")]
	public async Task<IActionResult> GetAll([FromRoute] int DepartmentId, CancellationToken cancellationToken = default)
	{
		var result = await _studentService.GetAllAsync(DepartmentId,cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("")]
	public async Task<IActionResult> Create([FromRoute]int DepartmentId,[FromBody] StudentRequest request, CancellationToken cancellationToken)
	{
		var result = await _studentService.AddAsync(DepartmentId,request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { DepartmentId, id = result.Value.Id,result.Value }, result.Value) : result.ToProblem();
	}
	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] int DepartmentId, [FromRoute] Guid id, [FromBody] UpdateStudentRequest request, CancellationToken cancellationToken)
	{
		var result = await _studentService.UpdateAsync(DepartmentId,id,request, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/assign-to-department")]
	public async Task<IActionResult> AssignStudentToDepartment([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var result = await _studentService.AssignStudentToDepartmentAsync(DepartmentId, id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/toggleStatus")]
	public async Task<IActionResult> ToggleStatus([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var result = await _studentService.ToggleStatusAsync(DepartmentId,id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

}
