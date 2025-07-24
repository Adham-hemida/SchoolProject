using FluentValidation;
using SchoolProject.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.User;
public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
	public ChangePasswordRequestValidator()
	{
		RuleFor(x => x.CurrentPassword)
			.NotEmpty();

		RuleFor(x => x.NewPassword)
			.NotEmpty()
			.Matches(RegexPatterns.Password)
			.WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase")
			.NotEqual(x => x.CurrentPassword)
			.WithMessage("New password should not be same as current password");
	}


}