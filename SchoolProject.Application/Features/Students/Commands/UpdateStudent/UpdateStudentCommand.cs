using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;

namespace SchoolProject.Application.Features.Students.Commands.UpdateStudent;
public record UpdateStudentCommand(int departmentId, Guid id, UpdateStudentRequest Request)
	: IRequest<Result>;

