using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class AssignmentErrors
{
	public static readonly Error DuplicatedAssignment =
	new("Assignment.Duplicated", " Another Assignment is already exist", StatusCodes.Status409Conflict);

	public static readonly Error AssignmentNotFound =
	new("Assignment.not_found", "No Assignment was found with the given Id", StatusCodes.Status404NotFound);

}
