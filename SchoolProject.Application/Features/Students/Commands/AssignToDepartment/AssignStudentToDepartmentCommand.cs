using MediatR;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.Features.Students.Commands.AssignToDepartment;
public record AssignStudentToDepartmentCommand(int departmentId, Guid id)
	: IRequest<Result>;
