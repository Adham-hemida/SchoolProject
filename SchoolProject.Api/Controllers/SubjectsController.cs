using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Infrastructure.Implementation.Services;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SubjectsController(ISubjectService subjectService) : ControllerBase
{
	private readonly ISubjectService _subjectService = subjectService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
	{
		var result = await _subjectService.GetByIdAsync(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
