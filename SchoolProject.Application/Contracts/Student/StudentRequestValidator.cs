using FluentValidation;

namespace SchoolProject.Application.Contracts.Student;
public class StudentRequestValidator : AbstractValidator<StudentRequest>
{
	public StudentRequestValidator()
	{
		RuleFor(x=>x.FirstName)
			.NotEmpty()
			.Length(3,50)
			.WithMessage("min is 3 and Max 50 characters");
		
		
		RuleFor(x=>x.LastName)
			.NotEmpty()
			.Length(3,50)
			.WithMessage("min is 3 and Max 50 characters.");

		RuleFor(x=>x.Phone)
			.NotEmpty()
			.WithMessage("Phone number is required.")
			.Length(11).WithMessage("Phone number must be exactly 11 digits.")
	        .Matches(@"^\d{11}$").WithMessage("Phone number must contain only digits.");

		RuleFor(x=>x.Address)
			.NotEmpty()
			.MaximumLength(100)
			.WithMessage("Address must not exceed 100 characters.");

		RuleFor(x => x.SubjectIds)
			.NotNull();

		RuleFor(x => x.SubjectIds)
			.Must(x => x.Count > 1)
			.WithMessage("should be at least 2 subjects")
			.When(x => x.SubjectIds != null);

		RuleForEach(x => x.SubjectIds)
			.GreaterThan(0)
			.WithMessage("number should be equal or  greater than 1 ");
	}
}
