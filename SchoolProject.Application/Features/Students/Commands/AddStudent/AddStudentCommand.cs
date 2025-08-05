using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Students.Commands.AddStudent;
public record AddStudentCommand(int departmentId, StudentRequest request)
	: IRequest<Result<StudentBasicResponse>>; 

