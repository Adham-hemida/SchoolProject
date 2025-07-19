using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.FileAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IFileAttachmentService
{
	Task<Result<Guid>> UploadAssignmentFileAsync(Guid assignmentId, UploadFileRequest file, CancellationToken cancellationToken);
	Task<Result> UploadStudentSubmissionAsync(Guid assignmentId, Guid studentId, UploadFileRequest file, CancellationToken cancellationToken);
}
