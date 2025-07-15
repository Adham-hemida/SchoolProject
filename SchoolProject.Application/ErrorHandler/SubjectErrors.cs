using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.ErrorHandler;
public static class SubjectErrors
{

	public static readonly Error NoValidSubjects =
	new("Subject.not_valid", "No subject was found ", StatusCodes.Status404NotFound);

}
