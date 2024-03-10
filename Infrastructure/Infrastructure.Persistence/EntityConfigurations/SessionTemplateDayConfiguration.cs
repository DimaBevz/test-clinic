using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class SessionTemplateDayConfiguration : IEntityTypeConfiguration<SessionTemplateDay>
    {
        public void Configure(EntityTypeBuilder<SessionTemplateDay> builder)
        {
            builder.ToTable("session_template_days");

            builder.Property(s => s.Id).HasColumnName("session_template_day_id");
            builder.Property(s => s.DayOfWeek).HasColumnName("day_of_week");
            builder.Property(s => s.IsActive).HasColumnName("is_active");
            builder.Property(s => s.TimetableTemplateId).HasColumnName("timetable_id");
        }
    }
}
