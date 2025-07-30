namespace SchoolProject.Application.Contracts.Authentication;
public record RoleResponse(
	string Id,
	string Name,
	bool IsDeleted
	);