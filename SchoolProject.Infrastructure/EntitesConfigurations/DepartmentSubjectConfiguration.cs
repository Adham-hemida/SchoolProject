namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmentSubject>
{
	public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
	{
		builder.Property(x=>x.IsMandatory).IsRequired();
	}
}
