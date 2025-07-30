using SchoolProject.Application.Contracts.Authentication;

namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IRoleService
{
	Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
}
