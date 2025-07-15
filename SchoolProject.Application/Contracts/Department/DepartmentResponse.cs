using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Department;
public record DepartmentResponse
(
	int Id,
	string Name,
	bool IsActive,
	string Description
);