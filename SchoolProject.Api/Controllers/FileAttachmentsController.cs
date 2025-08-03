using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Contracts.Assignment;
using SchoolProject.Application.Contracts.FileAttachment;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using SchoolProject.Application.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using SchoolProject.Application.Abstractions.Consts;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FileAttachmentsController(IFileAttachmentService fileAttachmentService) : ControllerBase
{
	private readonly IFileAttachmentService _fileAttachmentService = fileAttachmentService;
	[HttpPost("assignment/{assignmentId}/upload")]
	public async Task<IActionResult> Upload([FromRoute] Guid assignmentId, [FromForm] UploadFileRequest request, CancellationToken cancellationToken)
	{
		var result = await _fileAttachmentService.UploadAssignmentFileAsync(assignmentId,request, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem(); ;
	}

	[EnableRateLimiting(RateLimiters.Concurrency)]
	[HttpPost("assignment/{assignmentId}/upload-assignment")]
	public async Task<IActionResult> UploadStudentSubmission([FromRoute] Guid assignmentId, [FromForm] UploadFileRequest request, CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();
		var result = await _fileAttachmentService.UploadStudentSubmissionAsync(assignmentId, userId!, request, cancellationToken);
		return result.IsSuccess ? Created() : result.ToProblem();
	}

	[EnableRateLimiting(RateLimiters.Concurrency)]
	[HttpGet("assignment/{assignmentId}/subject/{subjectId}/download/{fileId}")]
	public async Task<IActionResult> DownLoadAssignmentFile([FromRoute] Guid fileId, [FromRoute] Guid assignmentId, [FromRoute] int subjectId, CancellationToken cancellationToken)
	{
		var result = await _fileAttachmentService.DownloadAssignmentFileAsync(fileId, assignmentId,subjectId, cancellationToken);
		return result.IsSuccess ? File(result.Value.fileContent ,result.Value.contentType,result.Value.fileName) :result.ToProblem() ;
	}

	[HttpGet("download/{fileId}")]
	public async Task<IActionResult> DownloadSubmissionsFile([FromRoute] Guid fileId , CancellationToken cancellationToken)
	{
		var result = await _fileAttachmentService.DownloadSubmissionsFileAsync(fileId, cancellationToken);
		return result.IsSuccess ? File(result.Value.fileContent ,result.Value.contentType,result.Value.fileName) :result.ToProblem() ;
	}
}
