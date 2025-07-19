using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class StudentSubmissionErrors
{
	public static readonly Error StudentAlreadySubmitted =
	new("Student.Submitted", " Student already Submitted", StatusCodes.Status400BadRequest);

}
