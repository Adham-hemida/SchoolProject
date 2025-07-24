using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Authentication;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IAuthentication;
using System.Security.Cryptography;

namespace SchoolProject.Infrastructure.Implementation.Authentication;
public class AuthService(
UserManager<ApplicationUser> userManager,
SignInManager<ApplicationUser> signInManager,
IJwtProvider jwtProvider
) : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
	private readonly IJwtProvider _jwtProvider = jwtProvider;
	private readonly int _refreshTokenExpirationDays = 14;


	public async Task<Result<AuthResponse>> GetTokenAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
	{
		if (await _userManager.FindByEmailAsync(loginRequest.Email) is not { } user)
			return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

		if (user.IsDisabled)
			return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

		var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, isPersistent: false, lockoutOnFailure: false);
		if (result.Succeeded)
		{
			var (token, expiresIn) = _jwtProvider.GenerateJwtToken(user);

			var refreshToken = GenerateRefreshToken();
			var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);

			user.RefreshTokens.Add(new RefreshToken
			{
				Token = refreshToken,
				ExpiresOn = refreshTokenExpiration
			});

			await _userManager.UpdateAsync(user);
			var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);
			return Result.Success(response);
		}
		var error = result.IsNotAllowed
			? UserErrors.EmailNotConfirmed
			: result.IsLockedOut
			? UserErrors.LockedUser
			: UserErrors.InvalidCredentials;

		return Result.Failure<AuthResponse>(error);
	}
	public async Task<Result<AuthResponse>> GetRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken = default)
	{
		var userId = _jwtProvider.ValidateToken(refreshTokenRequest.token);
		if (userId is null)
			return Result.Failure<AuthResponse>(UserErrors.InvalidJwtTokens);

		var user = await _userManager.FindByIdAsync(userId);
		if (user is null)
			return Result.Failure<AuthResponse>(UserErrors.InvalidJwtTokens);

		if (user.IsDisabled)
			return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

		if (user.LockoutEnd > DateTime.UtcNow)
			return Result.Failure<AuthResponse>(UserErrors.LockedUser);

		var userRefreshToken = user.RefreshTokens.SingleOrDefault(x=>x.Token==refreshTokenRequest.refreshToken && x.IsActive);

		if(userRefreshToken is null)
			return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

		userRefreshToken.RevokedOn=DateTime.UtcNow;

		var (newToken, expiresIn) = _jwtProvider.GenerateJwtToken(user);

		var newRefreshToken = GenerateRefreshToken();
		var newRefreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);

		user.RefreshTokens.Add(new RefreshToken
		{
			Token = newRefreshToken,
			ExpiresOn = newRefreshTokenExpiration
		});

		await _userManager.UpdateAsync(user);
		var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, newRefreshTokenExpiration);
		return Result.Success(response);

	}


	public async Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken = default)
	{
		var userId = _jwtProvider.ValidateToken(refreshTokenRequest.token);
		if (userId is null)
			return Result.Failure(UserErrors.InvalidJwtTokens);

		var user = await _userManager.FindByIdAsync(userId);
		if (user is null)
			return Result.Failure(UserErrors.InvalidJwtTokens);

		if (user.IsDisabled)
			return Result.Failure(UserErrors.DisabledUser);

		if (user.LockoutEnd > DateTime.UtcNow)
			return Result.Failure(UserErrors.LockedUser);

		var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshTokenRequest.refreshToken && x.IsActive);

		if (userRefreshToken is null)
			return Result.Failure(UserErrors.InvalidRefreshToken);

		userRefreshToken.RevokedOn = DateTime.UtcNow;

		await _userManager.UpdateAsync(user);
		return Result.Success();

	}

	private static string GenerateRefreshToken()
	{
		return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
	}

}
