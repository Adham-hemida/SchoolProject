using Hangfire;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.ExceptionHandler;
using SchoolProject.Application.Settings;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text;

namespace SchoolProject.Api;

public static class DependencyInjection
{

	public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
	{

		services.AddBackgroundJobsConfig(configuration);

		services.AddOptions<MailSettings>()
	       .BindConfiguration(nameof(MailSettings))
	       .ValidateDataAnnotations()
	       .ValidateOnStart();



		services.AddOptions<JwtOptions>()
			 .Bind(configuration.GetSection(JwtOptions.sectionName))
			 .ValidateDataAnnotations()
			 .ValidateOnStart();
		// to use it to get the name of attributes in JwtOptions class
		var JwtSettings = configuration.GetSection(JwtOptions.sectionName).Get<JwtOptions>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

		})
			.AddJwtBearer(o =>
			{
				o.SaveToken = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidIssuer = JwtSettings?.Issuer,
					ValidAudience = JwtSettings?.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings?.Key!))
				};
			});

		services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequiredLength = 8;
			options.User.RequireUniqueEmail = true;
			options.SignIn.RequireConfirmedEmail = true;
		});



		return services;
	}

	private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHangfire(config => config
		   .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
		   .UseSimpleAssemblyNameTypeSerializer()
		   .UseRecommendedSerializerSettings()
		   .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnect")));

		services.AddHangfireServer();


		return services;
	}

}
