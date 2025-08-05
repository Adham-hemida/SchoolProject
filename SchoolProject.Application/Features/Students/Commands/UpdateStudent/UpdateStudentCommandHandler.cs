using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Application.Features.Students.Commands.UpdateStudent;
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result>
{
	private readonly IStudentService _studentService;

	public UpdateStudentCommandHandler(IStudentService studentService)
	{
		_studentService = studentService;
	}

	public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
	{
	return await _studentService.UpdateAsync(request.departmentId, request.id, request.Request, cancellationToken);
	}
}
