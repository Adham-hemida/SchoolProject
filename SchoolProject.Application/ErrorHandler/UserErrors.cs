using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class UserErrors
{
	public static readonly Error InvalidCredentials =
		new("invalid.credentials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);

	public static readonly Error DisabledUser =
        new("User.DisabledUser", "Disabled user, please contact your administrator", StatusCodes.Status401Unauthorized);

	public static readonly Error LockedUser =
	    new("User.LockedUser", "Locked user, please contact your administrator", StatusCodes.Status401Unauthorized);

	public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

	public static readonly Error InvalidJwtTokens =
	new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

	public static readonly Error InvalidRefreshToken =
	   new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

	public static readonly Error UserNotFound =
      new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);

}
