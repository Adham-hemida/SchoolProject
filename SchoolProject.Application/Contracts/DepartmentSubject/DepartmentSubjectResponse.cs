using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.DepartmentSubject;
public record DepartmentSubjectResponse(
	int Id,
	string Name,
	IEnumerable<SubjectDetailsResponse> Subject);
