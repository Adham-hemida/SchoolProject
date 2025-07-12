using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entites;

namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
	public void Configure(EntityTypeBuilder<Department> builder)
	{
		builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
		builder.Property(d => d.Description).HasMaxLength(500);
	}
}

