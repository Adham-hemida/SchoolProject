using SchoolProject.Application.Contracts.FileAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Assignment;
public record AssignmentResponse(
	Guid Id,
	string Title,
	string SubjectName,
	List<FileAttachmentResponse>FileAttachments
	);
