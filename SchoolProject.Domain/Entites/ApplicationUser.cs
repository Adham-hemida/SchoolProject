using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Domain.Entites;
public sealed class ApplicationUser : IdentityUser
{
	public ApplicationUser()
	{
		Id = Guid.CreateVersion7().ToString();
		SecurityStamp = Guid.CreateVersion7().ToString();

	}
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public bool IsDisabled { get; set; }

	public Student? Student { get; set; }
	public Teacher? Teacher { get; set; }
	public List<RefreshToken> RefreshTokens { get; set; } = [];
}
