using MediatR;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.Features.Students.Commands.ToggleStatus;
public record ToggleStudentStatusCommand(int departmentId, Guid id)
	: IRequest<Result>;
