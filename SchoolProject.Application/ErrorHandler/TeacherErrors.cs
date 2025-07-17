using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class TeacherErrors
{
	public static readonly Error TeacherNotFound =
         new("Teacher.not_found", "No Teacher was found with the given Id", StatusCodes.Status404NotFound);

	public static readonly Error DuplicatedTeacher =
		new("Teacher.Duplicated", " Another Teacher is already exist", StatusCodes.Status409Conflict);

}
