using FluentValidation;
using SchoolProject.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.User;
public class TeacherUserRequestValidator :AbstractValidator<TeacherUserRequest>
{
	public TeacherUserRequestValidator()
	{
		RuleFor(x => x.Email)
		.NotEmpty()
		.EmailAddress();

		RuleFor(x => x.Password)
				.NotEmpty()
				.Matches(RegexPatterns.Password)
				.WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");

		RuleFor(x => x.FirstName)
				.NotEmpty()
				.Length(3, 100);

		RuleFor(x => x.LastName)
				.NotEmpty()
				.Length(3, 100);

		RuleFor(x => x.Phone)
				.NotEmpty()
				.WithMessage("Phone number is required.")
				.Length(11).WithMessage("Phone number must be exactly 11 digits.")
				.Matches(@"^\d{11}$").WithMessage("Phone number must contain only digits.");

	}
}
