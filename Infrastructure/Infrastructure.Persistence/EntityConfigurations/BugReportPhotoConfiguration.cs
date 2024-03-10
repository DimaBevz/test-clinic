using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class BugReportPhotoConfiguration : IEntityTypeConfiguration<BugReportPhoto>
{
    public void Configure(EntityTypeBuilder<BugReportPhoto> builder)
    {
        builder.ToTable("bug_report_photos");

        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.ContentType).HasColumnName("content_type");
        builder.Property(u => u.PresignedUrl).HasColumnName("presigned_url");
        builder.Property(u => u.PhotoObjectKey).HasColumnName("photo_object_key");
        builder.Property(d => d.ExpiresAt).HasColumnName("expires_at");
        builder.Property(u => u.BugReportId).HasColumnName("bug_report_id");
    }
}