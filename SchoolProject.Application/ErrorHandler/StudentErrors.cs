using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.ErrorHandler;
public static class StudentErrors
{
	public static readonly Error StudentNotFound =
	new("Student.not_found", "No Student was found with the given Id", StatusCodes.Status404NotFound);

}
