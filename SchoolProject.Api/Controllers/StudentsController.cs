using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Abstractions.Consts;
using SchoolProject.Application.Contracts.Common;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Features.Students.Commands.AddStudent;
using SchoolProject.Application.Features.Students.Commands.AssignToDepartment;
using SchoolProject.Application.Features.Students.Commands.ToggleStatus;
using SchoolProject.Application.Features.Students.Commands.UpdateStudent;
using SchoolProject.Application.Features.Students.Queries.GetAllStudents;
using SchoolProject.Application.Features.Students.Queries.GetStudentById;
using SchoolProject.Application.Interfaces.IServices;
namespace SchoolProject.Api.Controllers;
[Route("api/Department/{DepartmentId}[controller]")]
[ApiController]
[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Teacher.Name},{DefaultRoles.Student.Name}")]

public class StudentsController(IStudentService studentService,IMediator mediator) : ControllerBase
{
	private readonly IStudentService _studentService = studentService;
	private readonly IMediator _mediator = mediator;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var result = await _mediator.Send(new GetStudentByIdQuery(DepartmentId, id), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}


	[HttpGet("")]
	public async Task<IActionResult> GetAll([FromRoute] int DepartmentId, [FromQuery] RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var result = await _mediator.Send(new GetAllStudentsQuery(DepartmentId, filters), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}


	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPost("")]
	public async Task<IActionResult> Create([FromRoute] int DepartmentId, [FromBody] StudentRequest request, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new AddStudentCommand(DepartmentId, request), cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { DepartmentId, id = result.Value.Id, result.Value }, result.Value) : result.ToProblem();
	}

	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] int DepartmentId, [FromRoute] Guid id, [FromBody] UpdateStudentRequest request, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new UpdateStudentCommand(DepartmentId, id, request), cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPut("{id}/assign-to-department")]
	public async Task<IActionResult> AssignStudentToDepartment([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new AssignStudentToDepartmentCommand(DepartmentId, id), cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}


	[Authorize(DefaultRoles.Admin.Name)]
	[HttpPut("{id}/toggleStatus")]
	public async Task<IActionResult> ToggleStatus([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new ToggleStudentStatusCommand(DepartmentId, id), cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}	
	//[HttpPut("{id}/toggleStatus")]
	//public async Task<IActionResult> ToggleStatus([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken)
	//{
	//	var result = await _studentService.ToggleStatusAsync(DepartmentId,id, cancellationToken);
	//	return result.IsSuccess ? NoContent() : result.ToProblem();
	//}

}
