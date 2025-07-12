namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
	public void Configure(EntityTypeBuilder<Teacher> builder)
	{
		builder.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
		builder.Property(t => t.LastName).IsRequired().HasMaxLength(100);
		builder.Property(t => t.Email).HasMaxLength(100);
		builder.Property(t => t.Phone).HasMaxLength(15);

	}
}

