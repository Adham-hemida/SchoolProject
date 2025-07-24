using Mapster;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Authentication;
using SchoolProject.Application.Interfaces.IAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Authentication;
public class AccountService(UserManager<ApplicationUser> userManager) : IAccountService
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<Result<UserProfileResponse>> GetProfileInfoAsync(string userId,CancellationToken cancellationToken=default)
	{
		var user = await _userManager.Users
		   .Where(x => x.Id == userId)
		   .ProjectToType<UserProfileResponse>()
		   .SingleAsync(cancellationToken);

		return Result.Success(user);
	}
}
