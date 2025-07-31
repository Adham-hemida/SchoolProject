namespace SchoolProject.Application.Contracts.User;
public record CreateUserRequest(
	string FirstName,
	string LastName,
	string Email,
	string Password
	);
