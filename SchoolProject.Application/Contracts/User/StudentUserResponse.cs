namespace SchoolProject.Infrastructure.Implementation.Authentication;
public record StudentUserResponse(
	string Id,
	string FirstName,
	string LastName,
	string Email,
	bool IsDisabled,
	string Role);
