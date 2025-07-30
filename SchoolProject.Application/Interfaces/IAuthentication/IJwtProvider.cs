using SchoolProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IAuthentication;
public interface IJwtProvider
{
	(string token, int expiresIn) GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles);
	string? ValidateToken(string token);
}
