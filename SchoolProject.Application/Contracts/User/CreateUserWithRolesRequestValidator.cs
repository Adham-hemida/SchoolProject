using FluentValidation;
using SchoolProject.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.User;
public class CreateUserWithRolesRequestValidator :AbstractValidator<CreateUserWithRolesRequest>
{
	public CreateUserWithRolesRequestValidator()
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

		RuleFor(x => x.Roles)
				.NotNull()
				.NotEmpty();

		RuleFor(x => x.Roles)
				.Must(x => x.Distinct().Count() == x.Count)
				.WithMessage("You cannot add duplicated role for the same user")
				.When(x => x.Roles != null);
	}
}

