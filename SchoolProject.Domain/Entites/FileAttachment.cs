namespace SchoolProject.Domain.Entites;
public class FileAttachment
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string FileName { get; set; } = string.Empty;
	public string StoredFileName { get; set; } = string.Empty;
	public string ContentType { get; set; } = string.Empty;
	public string FileExtension { get; set; } = string.Empty;
	public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

	public ICollection<StudentSubmission> StudentSubmissions { get; set; } = [];
	public ICollection<Assignment> Assignments { get; set; } = [];
}
