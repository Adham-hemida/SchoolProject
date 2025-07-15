namespace SchoolProject.Application.Contracts.Student;
public record StudentRequest(
	string FirstName,
	string LastName,
	string Address,
	string Phone,
	List<int> SubjectIds
	);
