using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.StudentSubject;
public class SudentSubjectRequestValidator:AbstractValidator<StudentSubjectRequest>
{
	public SudentSubjectRequestValidator()
	{
		RuleFor(x=>x.Grade)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.WithMessage("Grade must be greater than or equal to 0");

		RuleFor(x => x.IsPassed)
			.NotNull()
			.WithMessage("You must specify whether the student passed or not.");



	}
}
