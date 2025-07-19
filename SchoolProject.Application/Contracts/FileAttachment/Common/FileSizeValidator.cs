using FluentValidation;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.FileAttachment.Common;
public class FileSizeValidator : AbstractValidator<IFormFile>
{
	public FileSizeValidator()
	{
		RuleFor(x => x)
			.Must((request, context) => request.Length <= FileSettings.MaxFileSizeInBytes)
			.WithMessage($"File size should be less  or equal  {FileSettings.MaxFileSizeInMB} MB.")
			.When(x => x is not null);
	}
}