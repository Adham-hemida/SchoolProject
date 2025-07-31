using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.ErrorHandler;
public static class StudentErrors
{
	public static readonly Error StudentNotFound =
	new("Student.not_found", "No Student was found with the given Id", StatusCodes.Status404NotFound);

	public static readonly Error DuplicatedStudent =
		new("Student.Duplicated", " Another Student is already exist", StatusCodes.Status409Conflict);

	public static readonly Error AlreadyHasUser =
		new("Student.Duplicated", "  Student is already Has User", StatusCodes.Status409Conflict);
}

