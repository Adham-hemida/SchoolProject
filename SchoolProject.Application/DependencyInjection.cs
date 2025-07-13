using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Application.ExceptionHandler;

namespace SchoolProject.Application;
public static class DependencyInjection
{
	public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
	{
		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();
		return services;
	}
}
