using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeachersController(ITeacherService teacherService) : ControllerBase
{
	private readonly ITeacherService _teacherService = teacherService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var result = await _teacherService.GetByIdAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
