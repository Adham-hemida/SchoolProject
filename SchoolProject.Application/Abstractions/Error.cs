namespace SchoolProject.Application.Abstractions;
public record Error(string Code, string Description, int? statusCode)
{
	public static readonly Error None = new(string.Empty, string.Empty, null);
}
