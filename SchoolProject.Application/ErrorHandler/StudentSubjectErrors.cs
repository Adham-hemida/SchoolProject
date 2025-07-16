using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class StudentSubjectErrors
{
	public static readonly Error StudentSubjectNotFound =
		new("StudentSubject.not_found", "No StudentSubject was found with the given Id", StatusCodes.Status404NotFound);

	public static readonly Error StudentSubjectDuplicated =
new("StudentSubject.Duplicated", "Another StudenttSubject is already exist", StatusCodes.Status409Conflict);

}
