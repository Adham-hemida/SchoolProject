namespace SchoolProject.Domain.Entites;
public class Assignment
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string Title { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;

	public Guid SubjectId { get; set; }
	public Subject Subject { get; set; } = default!;

	public ICollection<StudentSubmission> Submissions { get; set; } = [];
	public ICollection<FileAttachment> FileAttachments { get; set; } = [];

}
