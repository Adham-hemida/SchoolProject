using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.User;
public record CreateUserWithRolesRequest(
	string FirstName,
	string LastName,
	string Email,
	string Password,
	IList<string> Roles
	);
