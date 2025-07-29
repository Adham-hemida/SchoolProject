using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions.Consts;

namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{
		// Default Data
		builder.HasData(new IdentityUserRole<string>
		{
			UserId = DefaultUsers.Admin.Id,
			RoleId = DefaultRoles.Admin.Id
		});
	}
}
