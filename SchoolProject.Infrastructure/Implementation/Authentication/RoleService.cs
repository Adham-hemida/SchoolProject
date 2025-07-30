using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Authentication;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IAuthentication;

namespace SchoolProject.Infrastructure.Implementation.Authentication;
public class RoleService(RoleManager<ApplicationRole> roleManager) :IRoleService
{
	private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

	public async Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default)
	{
		return await _roleManager.Roles
			.Where(x => (!x.IsDeleted || includeDisabled))
			.Select(x => new RoleResponse
			(x.Id,
			x.Name!,
			x.IsDeleted))
			.ToListAsync(cancellationToken);
	}

	public async Task<Result<RoleResponse>> GetAsync(string id)
	{
		if (await _roleManager.FindByIdAsync(id) is not { } role)
			return Result.Failure<RoleResponse>(RolesError.RoleNotFound);


		var response = new RoleResponse(role.Id, role.Name!, role.IsDeleted);

		return Result.Success(response);
	}
}
