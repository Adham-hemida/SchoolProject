using FluentValidation;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.FileAttachment.Common;
public class BlockedSignatureValidator : AbstractValidator<IFormFile>
{
	public BlockedSignatureValidator()
	{
		RuleFor(x => x)
			.Must((request, context) =>
			{
				BinaryReader binary = new(request.OpenReadStream());
				var bytes = binary.ReadBytes(2);

				var fileSequenceHex = BitConverter.ToString(bytes);
				foreach (var signature in FileSettings.BlockSignatures)
				{
					if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
						return false;
				}
				return true;
			})
			.WithMessage("Not allow file content")
			.When(x => x is not null);
	}
}
