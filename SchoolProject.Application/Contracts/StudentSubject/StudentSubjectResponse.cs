using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.StudentSubject;
public record StudentSubjectResponse(
	string SubjectName,
	double? Grade,
	bool IsPassed
);

