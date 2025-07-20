using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Contracts.Assignment;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Abstractions;
using SchoolProject.Infrastructure.Implementation.Services;


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
}
