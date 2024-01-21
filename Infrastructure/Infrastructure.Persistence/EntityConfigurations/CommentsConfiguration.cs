using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class CommentsConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            builder.Property(c => c.Id).HasColumnName("comment_id");
            builder.Property(c => c.CommentText).HasColumnName("comment_text");
            builder.Property(c => c.Rating).HasColumnName("rating");
            builder.Property(c => c.PatientDataId).HasColumnName("patient_data_id");
            builder.Property(c => c.PhysicianDataId).HasColumnName("physician_data_id");
            builder.Property(c => c.CreatedAt).HasColumnName("created_at");
        }
    }
}
