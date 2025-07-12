namespace SchoolProject.Domain.Entites;

public class Department
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } 
	public bool IsActive { get; set; } = true;
	
	public ICollection<Student> Students { get; set; } = [];
	public ICollection<DepartmentSubject> DepartmentSubjects { get; set; } =[];

}
