namespace SchoolProject.Domain.Entites;
public class StudentSubject
{
	public int Id { get; set; }
	public double? Grade { get; set; }
	public bool IsPassed { get; set; } = false;
	public bool IsActive { get; set; } = true;

	public Guid StudentId { get; set; }
	public Student Student { get; set; } = default!;

	public int SubjectId { get; set; }
	public Subject Subject { get; set; } = default!;
}
