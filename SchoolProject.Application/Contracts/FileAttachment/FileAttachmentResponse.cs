using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.FileAttachment;
public record FileAttachmentResponse(
	Guid Id,
	string DownLoadUrl);
