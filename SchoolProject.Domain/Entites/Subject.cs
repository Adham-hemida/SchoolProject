namespace SchoolProject.Domain.Entites;

public class Subject
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public bool IsActive { get; set; } = true;
	public int CreditHours { get; set; }
	public string? Description { get; set; } = default!;

	public Guid TeacherId { get; set; }
	public Teacher Teacher { get; set; } = default!;

	public ICollection<StudentSubject> StudentsSubjects { get; set; } = [];
	public ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = [];


}
