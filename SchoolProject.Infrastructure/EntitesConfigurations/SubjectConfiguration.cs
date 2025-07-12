using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entites;

namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
	public void Configure(EntityTypeBuilder<Subject> builder)
	{
		builder.Property(x => x.Name).HasMaxLength(100);
		builder.Property(x => x.Description).HasMaxLength(500);
		builder.Property(x => x.CreditHours).IsRequired();
	}
}
