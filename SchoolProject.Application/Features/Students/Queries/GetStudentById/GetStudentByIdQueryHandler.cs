using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Students.Queries.GetStudentById;
public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentResponse>>
{
	private readonly IStudentService _studentService;

	public GetStudentByIdQueryHandler(IStudentService studentService)
	{
		_studentService = studentService;
	}

	public async Task<Result<StudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
	{
		return await _studentService.GetAsync(request.departmentId, request.Id, cancellationToken);
	}
}
