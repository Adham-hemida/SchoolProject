using FluentValidation;

namespace SchoolProject.Application.Contracts.Student;
public class StudentRequestValidator : AbstractValidator<StudentRequest>
{
	public StudentRequestValidator()
	{
		RuleFor(x=>x.FirstName)
			.NotEmpty().WithMessage("First name is required.")
			.Length(3,50)
			.WithMessage("Max is 50 characters.");
		
		
		RuleFor(x=>x.LastName)
			.NotEmpty().WithMessage("First name is required.")
			.Length(3,50)
			.WithMessage("Max 50 characters.");

		RuleFor(x=>x.Phone)
			.NotEmpty()
			.WithMessage("Phone number is required.")
			.MaximumLength(15)
			.WithMessage("Phone number must not exceed 15 characters.");

		RuleFor(x=>x.Address)
			.NotEmpty()
			.MaximumLength(100)
			.WithMessage("Address must not exceed 100 characters.");
	}
}
