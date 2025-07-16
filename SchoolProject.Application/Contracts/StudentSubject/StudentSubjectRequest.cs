using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.StudentSubject;
public record StudentSubjectRequest(
	double Grade,
	bool IsPassed 
	);
