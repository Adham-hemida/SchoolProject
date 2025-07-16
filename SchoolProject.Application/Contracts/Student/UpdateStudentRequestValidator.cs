using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Student;
public class UpdateStudentRequestValidator:AbstractValidator<UpdateStudentRequest>
{
	public UpdateStudentRequestValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty()
			.Length(3, 50)
			.WithMessage("min is 3 and Max 50 characters");


		RuleFor(x => x.LastName)
			.NotEmpty()
			.Length(3, 50)
			.WithMessage("min is 3 and Max 50 characters.");

		RuleFor(x => x.Phone)
			.NotEmpty()
			.WithMessage("Phone number is required.")
			.Length(11).WithMessage("Phone number must be exactly 11 digits.")
			.Matches(@"^\d{11}$").WithMessage("Phone number must contain only digits.");

		RuleFor(x => x.Address)
			.NotEmpty()
			.MaximumLength(100)
			.WithMessage("Address must not exceed 100 characters.");
	}
}
