using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Students.Commands.AddStudent;
public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, Result<StudentBasicResponse>>
{
	private readonly IStudentService _studentService;

	public AddStudentCommandHandler(IStudentService studentService)
	{
		_studentService = studentService;
	}
	public async Task<Result<StudentBasicResponse>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
	{
		return await _studentService.AddAsync(request.departmentId,request.request, cancellationToken);
	}
}
