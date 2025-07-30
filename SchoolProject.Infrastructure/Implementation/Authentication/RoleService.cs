using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Role;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IAuthentication;

namespace SchoolProject.Infrastructure.Implementation.Authentication;
public class RoleService(RoleManager<ApplicationRole> roleManager) : IRoleService
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

	public async Task<Result<RoleResponse>> AddAsync(RoleRequest request)
	{
		var roleIsExists = await _roleManager.RoleExistsAsync(request.Name);

		if (roleIsExists)
			return Result.Failure<RoleResponse>(RolesError.RoleDuplicated);

		var role = new ApplicationRole
		{
			Name = request.Name,
			ConcurrencyStamp = Guid.CreateVersion7().ToString()
		};

		var result = await _roleManager.CreateAsync(role);

		if(!result.Succeeded)
		{
			var errors = result.Errors.First();
			return Result.Failure<RoleResponse>(new Error(errors.Code, errors.Description, StatusCodes.Status400BadRequest));
		}

		return Result.Success(new RoleResponse(role.Id, role.Name!, role.IsDeleted));

	}
}