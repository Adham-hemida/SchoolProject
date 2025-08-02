using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using SchoolProject.Application.Settings;

namespace SchoolProject.Infrastructure.Health;
public class MailProviderHealthCheck(IOptions<MailSettings> mailSettings) : IHealthCheck
{
	private readonly MailSettings _mailSettings = mailSettings.Value;
	public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		try
		{
			using var smtp = new SmtpClient();
			smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls, cancellationToken);
			smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password, cancellationToken);

			return Task.FromResult(HealthCheckResult.Healthy());
		}
		catch (Exception ex)
		{
			return Task.FromResult(HealthCheckResult.Unhealthy(exception: ex));
		}
	}
}
