
namespace SchoolProject.Application.Contracts.Student;
public record StudentResponse(
	string FirstName,
	string LastName,
	string Address,
	string Phone,
	IEnumerable<StudentSubjectResponse> StudentSubjects
	);

