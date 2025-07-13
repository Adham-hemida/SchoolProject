using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SchoolProject.Application.ExceptionHandler;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	private readonly ILogger<GlobalExceptionHandler> _logger = logger;

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		_logger.LogError(exception, "SomeThing went wrong : {Message}", exception.Message);
		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Title = "Internal Server Error",
			Type = "https://www.rfc-editor.org/rfc/rfc9110#status.500"
		};
		httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
		return true;
	}
}
