using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{

	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(x => x.FirstName).HasMaxLength(100);
		builder.Property(x => x.LastName).HasMaxLength(100);
	}
}