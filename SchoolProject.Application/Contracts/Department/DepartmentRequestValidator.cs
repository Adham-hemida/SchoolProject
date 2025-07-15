using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Department;
public class DepartmentRequestValidator:AbstractValidator<DepartmentRequest>
{
	public DepartmentRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");
	}
}
