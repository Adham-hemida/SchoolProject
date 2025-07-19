namespace SchoolProject.Domain.Entites;
public class FileAttachment
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string FileName { get; set; } = string.Empty;
	public string StoredFileName { get; set; } = string.Empty;
	public string ContentType { get; set; } = string.Empty;
	public string FileExtension { get; set; } = string.Empty;
	public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

	public Guid AssignmentId { get; set; }
	public Assignment Assignment { get; set; } = default!;

	public ICollection<StudentSubmission> StudentSubmissions { get; set; } = [];
}
