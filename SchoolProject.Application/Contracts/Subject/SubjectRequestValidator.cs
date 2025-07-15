using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Subject;
public class SubjectRequestValidator:AbstractValidator<SubjectRequest>
{
	public SubjectRequestValidator()
	{
		RuleFor(x=>x.CreditHours)
			.GreaterThan(0)
			.WithMessage("Credit hours must be greater than 0.")
			.LessThanOrEqualTo(4)
			.WithMessage("Credit hours cannot exceed 4.");

		RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(100)
			.WithMessage("Subject name cannot exceed 100 characters.");

		RuleFor(x => x.TeacherId)
			.GreaterThan(Guid.Empty)
			.WithMessage("Teacher ID is required."); ;
	}
}
