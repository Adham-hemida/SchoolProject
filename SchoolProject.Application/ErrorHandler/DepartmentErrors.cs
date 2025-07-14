using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.ErrorHandler;
public static class DepartmentErrors
{
	public static readonly Error DepartmentNotFound =
		new("Department.not_found", "No Department was found with the given Id", StatusCodes.Status404NotFound);
}
