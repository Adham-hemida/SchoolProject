namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class AssigmentConfiguration : IEntityTypeConfiguration<Assignment>
{
	public void Configure(EntityTypeBuilder<Assignment> builder)
	{
		builder.Property(x => x.Title).HasMaxLength(100);
	}
}
