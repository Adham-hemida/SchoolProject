namespace SchoolProject.Domain.Entites;
public class DepartmentSubject
{
	public int Id { get; set; }
	public bool IsMandatory { get; set; }

	public int DepartmentId { get; set; }
	public virtual Department Department { get; set; } = default!;

	public int SubjectId { get; set; }
	public Subject Subject { get; set; } = default!;
}
