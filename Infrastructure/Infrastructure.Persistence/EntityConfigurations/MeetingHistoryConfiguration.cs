using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class MeetingHistoryConfiguration : IEntityTypeConfiguration<MeetingHistory>
    {
        public void Configure(EntityTypeBuilder<MeetingHistory> builder)
        {
            builder.ToTable("meeting_history");

            builder.Property(mh => mh.SessionId).HasColumnName("session_id");
            builder.Property(mh => mh.Title).HasColumnName("title");
            builder.Property(mh => mh.CreatedAt).HasColumnName("created_at");
            builder.Property(mh => mh.StartedAt).HasColumnName("started_at");
            builder.Property(mh => mh.EndedAt).HasColumnName("ended_at");
        }
    }
}
