namespace SchoolProject.Infrastructure.EntitesConfigurations;
public class FileAttachmentConfiguration:IEntityTypeConfiguration<FileAttachment>
{
	public void Configure(EntityTypeBuilder<FileAttachment> builder)
	{
		builder.Property(x => x.FileName).HasMaxLength(250);
		builder.Property(x => x.StoredFileName).HasMaxLength(250);
		builder.Property(x => x.ContentType).HasMaxLength(50);
		builder.Property(x => x.FileExtension).HasMaxLength(10);

	}
}
