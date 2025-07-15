namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
{
	public void Configure(EntityTypeBuilder<StudentSubject> builder)
	{
		
		//builder.HasIndex(x => new { x.StudentId, x.SubjectId }).IsUnique();
	}
}

