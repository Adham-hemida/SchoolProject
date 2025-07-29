using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Contracts.Subject;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Infrastructure.Implementation.Services;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubjectsController(ISubjectService subjectService) : ControllerBase
{
	private readonly ISubjectService _subjectService = subjectService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
	{
		var result = await _subjectService.GetByIdAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("")]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
	{
		var result = await _subjectService.GetAllAsync(cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("")]
	public async Task<IActionResult> Create([FromBody] SubjectRequest request, CancellationToken cancellationToken)
	{
		var result = await _subjectService.AddAsync(request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SubjectRequest request, CancellationToken cancellationToken)
	{
		var result = await _subjectService.UpdateAsync(id, request, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}


	[HttpPut("{id}/toggleStatus")]
	public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _subjectService.ToggleStatusAsync(id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
	
	[HttpPost("department/{departmentId}/subject/{id}/add-subject-to-department")]
	public async Task<IActionResult> AddSubjectToDepartment([FromRoute] int id, [FromRoute] int departmentId, [FromBody] bool isMandatory, CancellationToken cancellationToken)
	{
		var result = await _subjectService.AddSubjectToDepartmentAsync(id,departmentId, isMandatory, cancellationToken);
		return result.IsSuccess ? Ok(id) : result.ToProblem();
	}
	
	[HttpPut("department/{departmentId}/subject/{id}/toggleStatus-departmentSubject")]
	public async Task<IActionResult> ToggleStatusForDepartmentSubjec([FromRoute] int id, [FromRoute] int departmentId, CancellationToken cancellationToken)
	{
		var result = await _subjectService.ToggleStatusForDepartmentSubjectAsync(id,departmentId, cancellationToken);
		return result.IsSuccess ? NoContent(): result.ToProblem();
	}


	[HttpPost("student/{studentId}/subject/{id}/add-subject-to-student")]
	public async Task<IActionResult> AddSubjectToDepartment([FromRoute] int id, [FromRoute] Guid studentId, CancellationToken cancellationToken)
	{
		var result = await _subjectService.AddSubjectToStudentAsync(id, studentId, cancellationToken);
		return result.IsSuccess ? Ok(id) : result.ToProblem();
	}
	

	[HttpPut("department/{departmentID}/student/{studentId}/subject/{id}/toggleStatus-studentSubject")]
	public async Task<IActionResult> ToggleStatusForStudentSubject([FromRoute] int id, [FromRoute] int departmentID, [FromRoute] Guid studentId, CancellationToken cancellationToken)
	{
		var result = await _subjectService.ToggleStatusForStudentSubjectAsync(id, studentId, departmentID, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

}
