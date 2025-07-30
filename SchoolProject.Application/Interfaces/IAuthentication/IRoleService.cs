using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Role;

namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IRoleService
{
	Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
	Task<Result<RoleResponse>> GetAsync(string id);
	Task<Result<RoleResponse>> AddAsync(RoleRequest request);
	Task<Result> UpdateAsync(string id, RoleRequest request);
	Task<Result> ToggleSatausAsync(string id);
}
