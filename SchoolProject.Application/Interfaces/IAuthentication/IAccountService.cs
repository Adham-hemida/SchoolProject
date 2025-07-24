using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IAccountService
{
	Task<Result<UserProfileResponse>> GetProfileInfoAsync(string userId,CancellationToken cancellationToken= default);
	Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
