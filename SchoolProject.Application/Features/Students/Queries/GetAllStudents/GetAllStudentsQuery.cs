using MediatR;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Common;
using SchoolProject.Application.Contracts.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Students.Queries.GetAllStudents;
public record GetAllStudentsQuery(int departmentId, RequestFilters filters)
	: IRequest<Result<PaginatedList<StudentBasicResponse>>>;
