namespace SchoolProject.Domain.Entites;

public class Student
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string Address { get; set; } = default!;
	public string Phone { get; set; } = default!;
	public bool IsActive { get; set; } = true;
}
