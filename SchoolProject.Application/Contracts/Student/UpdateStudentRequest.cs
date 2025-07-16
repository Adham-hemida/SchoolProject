using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Student;
public record UpdateStudentRequest(
	string FirstName,
	string LastName,
	string Address,
	string Phone);
