using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Authentication;

namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IAuthService
{
	Task<Result<AuthResponse>> GetTokenAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default);
	Task<Result<AuthResponse>> GetRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken = default);
	Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken = default);
	Task<Result> SendResetPasswordCodeAsync(string email);
	Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
}
