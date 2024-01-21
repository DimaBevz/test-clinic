using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class UserPhotoDataConfiguration : IEntityTypeConfiguration<UserPhotoData>
    {
        public void Configure(EntityTypeBuilder<UserPhotoData> builder)
        {
            builder.ToTable("user_photo_data");

            builder.Property(u => u.UserId).HasColumnName("user_id");
            builder.Property(u => u.ContentType).HasColumnName("content_type");
            builder.Property(u => u.PresignedUrl).HasColumnName("presigned_url");
            builder.Property(u => u.PhotoObjectKey).HasColumnName("photo_object_key");
            builder.Property(d => d.ExpiresAt).HasColumnName("expires_at");
        }
    }
}
