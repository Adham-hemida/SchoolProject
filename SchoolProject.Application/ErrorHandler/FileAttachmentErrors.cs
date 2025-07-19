using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.ErrorHandler;
public static class FileAttachmentErrors
{
	public static readonly Error FileAttachmentNotFound =
	new(" FileAttachment.not_found", "No  FileAttachment was found ", StatusCodes.Status404NotFound);

}
