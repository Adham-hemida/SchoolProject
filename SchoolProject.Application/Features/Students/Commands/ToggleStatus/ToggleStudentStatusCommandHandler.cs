using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Application.Features.Students.Commands.ToggleStatus;
public class ToggleStudentStatusCommandHandler : IRequestHandler<ToggleStudentStatusCommand, Result>
{
	private readonly IStudentService _studentService;

	public ToggleStudentStatusCommandHandler(IStudentService studentService)
	{
		_studentService = studentService;
	}
	public async Task<Result> Handle(ToggleStudentStatusCommand request, CancellationToken cancellationToken)
	{
		return await _studentService.ToggleStatusAsync(request.departmentId, request.id, cancellationToken);
	}
}
