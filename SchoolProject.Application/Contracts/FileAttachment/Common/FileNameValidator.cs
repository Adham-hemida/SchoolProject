using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.FileAttachment.Common;
public class FileNameValidator : AbstractValidator<IFormFile>
{
	public FileNameValidator()
	{
		RuleFor(x => x.FileName)
			.Matches("^[A-Za-z0-9_\\-.]*$")
			.WithMessage("Invalid file name")
			.When(x => x is not null);
	}
}
