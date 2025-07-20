using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Assignment;
public record AssignmentSubmissionResponse(
	Guid FileId,
	string DownloadUrl
);
