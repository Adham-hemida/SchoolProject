namespace SchoolProject.Application.Contracts.User;

public record CreateStudentRequest(
	string FirstName,
	string LastName,
	string Email,
	string Password,
	string Address,
	string Phone,
	int DepartmentId
	);