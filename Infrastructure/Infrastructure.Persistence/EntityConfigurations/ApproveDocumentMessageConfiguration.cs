using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class ApproveDocumentMessageConfiguration : IEntityTypeConfiguration<ApproveDocumentMessage>
    {
        public void Configure(EntityTypeBuilder<ApproveDocumentMessage> builder)
        {
            builder.ToTable("approve_document_message");

            builder.Property(a => a.PhysicianDataId).HasColumnName("physician_data_id");
            builder.Property(a => a.Message).HasColumnName("message");
        }
    }
}
