using FluentValidation;
using SchoolProject.Application.Contracts.FileAttachment.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.FileAttachment;
public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
	public UploadFileRequestValidator()
	{
		RuleFor(x => x.File)
			.SetValidator(new FileSizeValidator())
			.SetValidator(new BlockedSignatureValidator())
			.SetValidator(new FileNameValidator());
	}
}