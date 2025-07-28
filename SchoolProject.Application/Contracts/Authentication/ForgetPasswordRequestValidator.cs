using FluentValidation;

namespace SchoolProject.Application.Contracts.Authentication;
public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
{
	public ForgetPasswordRequestValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
	}
}