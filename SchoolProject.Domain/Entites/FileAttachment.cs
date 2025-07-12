namespace SchoolProject.Domain.Entites;
public class FileAttachment
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string FileName { get; set; } = default!;
	public string StoredFileName { get; set; } = default!;
	public string ContentType { get; set; } = default!;
	public string FileExtension { get; set; } = default!;
	public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

	public ICollection<StudentSubmission> StudentSubmissions { get; set; } = [];
	public ICollection<Assignment> Assignments { get; set; } = [];
}
