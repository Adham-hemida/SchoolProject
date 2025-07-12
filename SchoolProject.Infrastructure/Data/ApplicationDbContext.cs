using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entites;
using System.Reflection;

namespace SchoolProject.Infrastructure.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):DbContext(options)
{
	public DbSet<Teacher> Teachers { get; set; } = default!;
	public DbSet<Subject> Subjects { get; set; } = default!;
	public DbSet<Student> Students { get; set; } = default!;
	public DbSet<Assignment> Assignments { get; set; } = default!;
	public DbSet<StudentSubject> StudentSubjects { get; set; } = default!;
	public DbSet<DepartmentSubject> DepartmentSubjects { get; set; } = default!;
	public DbSet<StudentSubmission> StudentSubmissions { get; set; } = default!;
	public DbSet<FileAttachment> FileAttachments { get; set; } = default!;
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(assembly:Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}

}
