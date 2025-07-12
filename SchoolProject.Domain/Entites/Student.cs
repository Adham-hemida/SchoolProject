namespace SchoolProject.Domain.Entites;

public class Student
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string Address { get; set; } = default!;
	public string Phone { get; set; } = default!;
	public string Email { get; set; } = default!;
	public bool IsActive { get; set; } = true;

	public int? DepartmentId { get; set; }
	public virtual Department Department { get; set; } = default!;

	public ICollection<StudentSubject> StudentsSubjects { get; set; } = [];
	public ICollection<StudentSubmission> Submissions { get; set; } = [];

}
