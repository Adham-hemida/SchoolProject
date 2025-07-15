using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Subject;
public record SubjectResponse (
	int Id,
	string Name,
	int CreditHours,
	Guid TeacherId
	);

