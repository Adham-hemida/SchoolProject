namespace SchoolProject.Domain.Entites;
public class Teacher
{
	public Guid Id { get; set; } = Guid.CreateVersion7();

	public string Name { get; set; } = default!;

	public string Email { get; set; } = default!;

	public ICollection<Subject> Subjects { get; set; } =[];
}
