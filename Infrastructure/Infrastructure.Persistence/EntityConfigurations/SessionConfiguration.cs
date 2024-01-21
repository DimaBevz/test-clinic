using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("sessions");

            builder.Property(s => s.Id).HasColumnName("session_id");
            builder.Property(s => s.PatientDataId).HasColumnName("patient_data_id");
            builder.Property(s => s.PhysicianDataId).HasColumnName("physician_data_id");
            builder.Property(s => s.StartTime).HasColumnName("start_time");
            builder.Property(s => s.EndTime).HasColumnName("end_time");
            builder.Property(s => s.IsArchived).HasColumnName("is_archived");
            builder.Property(s => s.IsDeleted).HasColumnName("is_deleted");
            builder.Property(s => s.MeetingId).HasColumnName("meeting_id");
        }
    }
}
