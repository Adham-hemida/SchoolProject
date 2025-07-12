namespace SchoolProject.Domain.Entites;
public class StudentSubmission
{
	public Guid Id { get; set; } = Guid.CreateVersion7();

	public Guid AssignmentId { get; set; }
	public Assignment Assignment { get; set; }=default!;

	public Guid StudentId { get; set; }
	public Student Student { get; set; } = default!;

	public Guid FileAttachmentId { get; set; }
	public FileAttachment FileAttachment { get; set; }=default!;

	public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}
