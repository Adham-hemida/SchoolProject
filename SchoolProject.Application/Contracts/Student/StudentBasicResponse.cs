namespace SchoolProject.Application.Contracts.Student;
public record StudentBasicResponse(
	Guid Id,
	string FirstName,
	string LastName,
	string Phone
	);

