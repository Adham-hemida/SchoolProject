using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Interfaces.IServices;
namespace SchoolProject.Api.Controllers;
[Route("api/Department/{DepartmentId}[controller]")]
[ApiController]
public class StudentsController(IStudentService studentService) : ControllerBase
{
	private readonly IStudentService _studentService = studentService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int DepartmentId, [FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var result = await _studentService.GetAsync(DepartmentId, id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
