using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class SessionTemplateTimesConfiguration : IEntityTypeConfiguration<SessionTemplateTimes>
    {
        public void Configure(EntityTypeBuilder<SessionTemplateTimes> builder)
        {
            builder.ToTable("session_template_times");

            builder.Property(s => s.Id).HasColumnName("session_template_times_id");
            builder.Property(s => s.StartTime).HasColumnName("start_time");
            builder.Property(s => s.EndTime).HasColumnName("end_time");
            builder.Property(s => s.SessionTemplateDayId).HasColumnName("session_template_day_id");
        }
    }
}
