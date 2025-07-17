using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Teacher;
public class TeacherRequestValidator :AbstractValidator<TeacherRequest>
{
	public TeacherRequestValidator()
	{       RuleFor(x => x.FirstName)
			.NotEmpty()
			.Length(3, 50)
			.WithMessage("min is 3 and Max 50 characters");


		RuleFor(x => x.LastName)
			.NotEmpty()
			.Length(3, 50)
			.WithMessage("min is 3 and Max 50 characters.");

		RuleFor(x => x.Phone)
			.Length(11).WithMessage("Phone number must be exactly 11 digits.")
			.Matches(@"^\d{11}$").WithMessage("Phone number must contain only digits.")
			.When(x => !string.IsNullOrEmpty(x.Phone));
		

		RuleFor(x => x.Email)
			.EmailAddress()
			.WithMessage("Invalid email format.")
			.When(x => !string.IsNullOrEmpty(x.Email));

	}
}
