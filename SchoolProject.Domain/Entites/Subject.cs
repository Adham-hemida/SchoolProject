namespace SchoolProject.Domain.Entites;

public class Subject
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;
	public int CreditHours { get; set; }
	public string? Description { get; set; } = string.Empty;

	public Guid TeacherId { get; set; }
	public Teacher Teacher { get; set; } = default!;

	public ICollection<StudentSubject> StudentsSubjects { get; set; } = [];
	public ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = [];
	public ICollection<Assignment> Assignments { get; set; } = [];


}
