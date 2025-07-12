using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entites;

namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class StudentConfigurations : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
		builder.Property(s => s.LastName).IsRequired().HasMaxLength(50);
		builder.Property(s => s.Address).IsRequired().HasMaxLength(200);
		builder.Property(s => s.Phone).IsRequired().HasMaxLength(15);
		builder.Property(s => s.Email).HasMaxLength(100);

	}
}
