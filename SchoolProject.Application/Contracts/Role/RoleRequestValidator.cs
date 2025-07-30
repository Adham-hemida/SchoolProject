using FluentValidation;

namespace SchoolProject.Application.Contracts.Role;
public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
	public RoleRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(3, 100);


	}
}