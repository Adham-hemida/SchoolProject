namespace SchoolProject.Domain.Entites;
public class Teacher
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string? Email { get; set; } 
	public string? Phone { get; set; }

	public ICollection<Subject> Subjects { get; set; } =[];
}
