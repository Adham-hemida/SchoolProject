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
	Task<Result> UploadStudentSubmissionAsync(Guid assignmentId, string userId, UploadFileRequest file, CancellationToken cancellationToken);
	Task<Result<(byte[] fileContent, string contentType, string fileName)>> DownloadAssignmentFileAsync(Guid fileId, Guid assignmentId, int subjectId,CancellationToken cancellationToken = default);
	Task<Result<(byte[] fileContent, string contentType, string fileName)>> DownloadSubmissionsFileAsync(Guid fileId,  CancellationToken cancellationToken = default);
}
