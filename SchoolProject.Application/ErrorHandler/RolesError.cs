using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;

namespace SchoolProject.Application.ErrorHandler;
public static class RolesError
{
	public static readonly Error RoleNotFound =
		new("Role.Notfound", "Role not found", StatusCodes.Status404NotFound);

	public static readonly Error RoleDuplicated =
		new("Role.RoleDuplicated", "Role already exists", StatusCodes.Status409Conflict);
}
