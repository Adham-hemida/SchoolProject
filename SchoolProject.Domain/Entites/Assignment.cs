namespace SchoolProject.Domain.Entites;
public class Assignment
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public string Title { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;

	public Guid? FileAttachmentId { get; set; } //  الملف المرفوع مع الواجب
	public FileAttachment? FileAttachment { get; set; } = default!;

	public ICollection<StudentSubmission> Submissions { get; set; } = [];
}
