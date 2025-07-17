using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Teacher;
public record TeacherResponse(
	Guid Id,
	string FirstName,
	string LastName,
	string? Email,
	string? Phone,
	bool IsActive,
	IEnumerable<string> Subjects
	);
