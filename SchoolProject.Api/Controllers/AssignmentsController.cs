using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Contracts.Assignment;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Abstractions;
using SchoolProject.Infrastructure.Implementation.Services;
using SchoolProject.Domain.Entites;


namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AssignmentsController(IAssignmentService assignmentService) : ControllerBase
{
	private readonly IAssignmentService _assignmentService = assignmentService;

	[HttpPost("{teacherId}")]
	public async Task<IActionResult> Create([FromRoute] Guid teacherId,AssignmentRequest request, CancellationToken cancellationToken)
	{
		var result = await _assignmentService.AddAsync(teacherId, request, cancellationToken);
		return result.IsSuccess ? Created() : result.ToProblem();
	}
	[HttpGet("{assignmentId}")]
	public async Task<IActionResult> GetById([FromRoute] Guid assignmentId, CancellationToken cancellationToken = default)
	{
		var result = await _assignmentService.GetByIdAsync(assignmentId, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}	
	
	[HttpGet("{assignmentId}/submissions")]
	public async Task<IActionResult> GetAssignmentSubmissions([FromRoute] Guid assignmentId, CancellationToken cancellationToken = default)
	{
		var result = await _assignmentService.GetAssignmentSubmissionsAsync(assignmentId, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPut("subject/{subjectId}/assignment/{assignmentId}")]
	public async Task<IActionResult> Update([FromRoute] Guid assignmentId, [FromRoute]int  subjectId, [FromBody] AssignmentUpdateRequest request, CancellationToken cancellationToken)
	{
		var result = await _assignmentService.UpdateAsync(assignmentId, subjectId, request, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}


	[HttpPut("{assignmentId}/toggleStatus")]
	public async Task<IActionResult> ToggleStatus([FromRoute] Guid assignmentId, CancellationToken cancellationToken)
	{
		var result = await _assignmentService.ToggleStatusAsync(assignmentId, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
