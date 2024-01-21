using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class SessionDetailConfiguration : IEntityTypeConfiguration<SessionDetail>
    {
        public void Configure(EntityTypeBuilder<SessionDetail> builder)
        {
            builder.ToTable("session_details");

            builder.Property(s => s.SessionId).HasColumnName("session_id");
            builder.Property(s => s.CurrentPainScale).HasColumnName("current_pain");
            builder.Property(s => s.AveragePainScaleLastMonth).HasColumnName("average_pain_last_month");
            builder.Property(s => s.HighestPainScaleLastMonth).HasColumnName("highest_pain_last_month");
            builder.Property(s => s.Complaints).HasColumnName("complaints");
            builder.Property(s => s.Treatment).HasColumnName("treatment");
            builder.Property(s => s.Recommendations).HasColumnName("recommendations");
            builder.Property(s => s.DiagnosisId).HasColumnName("diagnosis_id");
        }
    }
}
