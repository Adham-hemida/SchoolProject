using SchoolProject.Application.Abstractions.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
	public void Configure(EntityTypeBuilder<ApplicationRole> builder)
	{
		// Default Data
		builder.HasData([
			new ApplicationRole{
				Id = DefaultRoles.Admin.Id,
				Name = DefaultRoles.Admin.Name,
				NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
				ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp,
			},
			new ApplicationRole{
				Id = DefaultRoles.Member.Id,
				Name = DefaultRoles.Member.Name,
				NormalizedName = DefaultRoles.Member.Name.ToUpper(),
				ConcurrencyStamp = DefaultRoles.Member.ConcurrencyStamp,
				IsDefault=true
			}
			]);
	}
}