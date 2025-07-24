using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Extensions;
public static class UserExtensions
{
	public static string? GetUserId(this ClaimsPrincipal user)
	{
		return user.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}	