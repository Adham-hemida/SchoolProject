using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Application.Features.Students.Commands.AssignToDepartment;
public class AssignStudentToDepartmentCommandHandler : IRequestHandler<AssignStudentToDepartmentCommand, Result>
{
	private readonly IStudentService _studentService;

	public AssignStudentToDepartmentCommandHandler(IStudentService studentService)
	{
		_studentService = studentService;
	}
	public async Task<Result> Handle(AssignStudentToDepartmentCommand request, CancellationToken cancellationToken)
	{
		return await _studentService.AssignStudentToDepartmentAsync(request.departmentId, request.id, cancellationToken);
	}
}
