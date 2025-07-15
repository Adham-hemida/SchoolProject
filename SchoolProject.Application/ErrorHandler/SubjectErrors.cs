using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.ErrorHandler;
public static class SubjectErrors
{

	public static readonly Error NoValidSubjects =
	new("Subject.not_valid", "No subject was found ", StatusCodes.Status404NotFound);

	public static readonly Error SubjectNotFound =
	new("Subject.not_found", "No Subject was found with the given Id", StatusCodes.Status404NotFound);

	public static readonly Error SubjectDepartment =
		new("Subject.Duplicated", " Another Subject is already exist", StatusCodes.Status409Conflict);
}
