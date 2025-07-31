using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.User;
public record TeacherUserResponse(
	string Id,
	string FirstName,
	string LastName,
	string Email,
	string Phone,
	bool IsDisabled,
	string Role
);
