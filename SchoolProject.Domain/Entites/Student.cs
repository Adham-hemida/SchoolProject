namespace SchoolProject.Domain.Entites;

public class Student
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;

	public int? DepartmentId { get; set; }
	public virtual Department Department { get; set; } = default!;

	public string? UserId { get; set; }
	public ApplicationUser? User { get; set; }

	public ICollection<StudentSubject> StudentsSubjects { get; set; } = [];
	public ICollection<StudentSubmission> Submissions { get; set; } = [];

}
