using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Application.Interfaces.IAuthentication;
using SchoolProject.Application.Interfaces.IGenericRepository;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Implementation.Authentication;
using SchoolProject.Infrastructure.Implementation.GenericRepository;
using SchoolProject.Infrastructure.Implementation.Services;
using SchoolProject.Infrastructure.Implementation.UnitOfWork;

namespace SchoolProject.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		services
			.AddIdentity<ApplicationUser, ApplicationRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		services.AddSingleton<IJwtProvider,JwtProvider>();

		services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IStudentService,StudentService>();
		services.AddScoped<IDepartmentService, DepartmentService>();
		services.AddScoped<ISubjectService, SubjectService>();
		services.AddScoped<IStudentSubjectService, StudentSubjectService>();
		services.AddScoped<IDepartmentSubjectService, DepartmentSubjectService>();
		services.AddScoped<ITeacherService, TeacherService>();
		services.AddScoped<IAssignmentService, AssignmentService>();
		services.AddScoped<IFileAttachmentService, FileAttachmentService>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IEmailSender, EmailService>();
		services.AddScoped<IRoleService, RoleService>();

		services.AddHttpContextAccessor();
		return services;
	}
}

