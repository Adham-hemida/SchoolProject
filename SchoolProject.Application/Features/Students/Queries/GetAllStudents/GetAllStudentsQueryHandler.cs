using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Students.Queries.GetAllStudents;
public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, Result<PaginatedList<StudentBasicResponse>>>
{
	private readonly IStudentService _studentService;

	public GetAllStudentsQueryHandler(IStudentService studentService)
	{
		_studentService = studentService;
	}
	public async Task<Result<PaginatedList<StudentBasicResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
	{
		return await _studentService.GetAllAsync(request.departmentId,request.filters, cancellationToken);
	}
}
