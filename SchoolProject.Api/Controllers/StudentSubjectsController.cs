using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.StudentSubject;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentSubjectsController(IStudentSubjectService studentSubjectService) : ControllerBase
{
	private readonly IStudentSubjectService _studentSubjectService = studentSubjectService;

	[HttpPost("student/{studentId}/subject/{id}/add-grade-to-student")]
	public async Task<IActionResult> AddGradeToStudent([FromRoute] int id, [FromRoute] Guid studentId, [FromBody]StudentSubjectRequest request , CancellationToken cancellationToken)
	{
		var result = await _studentSubjectService.AddGradeToStudentAsync(id, studentId, request, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("{studentId}/get-grades-of-students")]
	public async Task<IActionResult> GetStudentWithAllSubjectsGrade( [FromRoute] Guid studentId , CancellationToken cancellationToken)
	{
		var result = await _studentSubjectService.GetStudentWithAllSubjectsGradeAsync( studentId, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}
