using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Application.Interfaces.IGenericRepository;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Implementation.GenericRepository;

namespace SchoolProject.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>));

		return services;
	}
}
