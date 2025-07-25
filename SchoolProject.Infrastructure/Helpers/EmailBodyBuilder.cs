namespace SchoolProject.Infrastructure.Helpers;
public static class EmailBodyBuilder
{
	public static string GenerateEmailBody(string template, Dictionary<string, string> templateModel)
	{
		var templatePath = $"{Directory.GetCurrentDirectory()}/Templates/{template}.html";

		var streamReader = new StreamReader(templatePath);
		var emailBody = streamReader.ReadToEnd();
		streamReader.Close();

		foreach (var item in templateModel)
		{
			emailBody = emailBody.Replace(item.Key, item.Value);
		}
		return emailBody;
	}
}