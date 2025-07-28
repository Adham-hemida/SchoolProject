using FluentValidation;
using SchoolProject.Application.Settings;

namespace SchoolProject.Application.Contracts.Authentication;
public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
	public ResetPasswordRequestValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();


		RuleFor(x => x.NewPassword)
			.Matches(RegexPatterns.Password)
			.NotEmpty()
			.WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");


		RuleFor(x => x.Code)
			.NotEmpty();
	}
}
