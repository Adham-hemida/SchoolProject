using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Abstractions.Consts;
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

		builder.OwnsMany(x => x.RefreshTokens)
	    .ToTable("RefreshTokens")
	    .WithOwner()
	    .HasForeignKey("UserId");



		builder.HasData(new ApplicationUser
		{
			Id = DefaultUsers.Admin.Id,
			FirstName = "School System",
			LastName = "Admin",
			UserName = DefaultUsers.Admin.Email,
			NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
			Email = DefaultUsers.Admin.Email,
			NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
			SecurityStamp = DefaultUsers.Admin.SecurityStamp,
			ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
			EmailConfirmed = true,
			PasswordHash = DefaultUsers.Admin.PasswordHash
		});
	}
}