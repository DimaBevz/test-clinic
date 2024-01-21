using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("documents");

            builder.Property(d => d.Id).HasColumnName("document_id");
            builder.Property(d => d.Title).HasColumnName("title");
            builder.Property(d => d.Type).HasColumnName("type");

            builder.Property(d => d.DocumentObjectKey).HasColumnName("document_object_key");
            builder.Property(d => d.ContentType).HasColumnName("content_type");
            builder.Property(d => d.PresignedUrl).HasColumnName("presigned_url");
            builder.Property(d => d.ExpiresAt).HasColumnName("expires_at");

            builder.Property(d => d.UserId).HasColumnName("user_id");
            builder.Property(d => d.SessionDetailSessionId).HasColumnName("session_id");
        }
    }
}
