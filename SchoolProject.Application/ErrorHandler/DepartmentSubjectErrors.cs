using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class DepartmentSubjectErrors
{
	public static readonly Error DepartmentSubjectNotFound =
		new("DepartmentSubject.not_found", "No DepartmentSubject was found with the given Id", StatusCodes.Status404NotFound);
			
			public static readonly Error DepartmentSubjectDuplicated =
		new("DepartmentSubject.Duplicated", "Another DepartmentSubject is already exist", StatusCodes.Status409Conflict);
}
