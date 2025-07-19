using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Contracts.Assignment;
using SchoolProject.Application.Contracts.FileAttachment;
using SchoolProject.Application.Interfaces.IServices;

namespace SchoolProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileAttachmentsController(IFileAttachmentService fileAttachmentService) : ControllerBase
{
	private readonly IFileAttachmentService _fileAttachmentService = fileAttachmentService;
	[HttpPost("assignment/{assignmentId}/upload")]
	public async Task<IActionResult> Upload([FromRoute] Guid assignmentId, [FromForm] UploadFileRequest request, CancellationToken cancellationToken)
	{
		var result = await _fileAttachmentService.UploadAssignmentFileAsync(assignmentId,request, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem(); ;
	}
}
