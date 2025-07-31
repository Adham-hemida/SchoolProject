using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.User;
public record TeacherUserRequest(
	string FirstName,
	string LastName,
	string Phone,
	string Email,
	string Password);