using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class SessionDayTemplateConfiguration : IEntityTypeConfiguration<SessionDayTemplate>
    {
        public void Configure(EntityTypeBuilder<SessionDayTemplate> builder)
        {
            builder.ToTable("session_day_templates");

            builder.Property(s => s.Id).HasColumnName("session_day_template_id");
            builder.Property(s => s.DayOfWeek).HasColumnName("day_of_week");
            builder.Property(s => s.IsActive).HasColumnName("is_active");
            builder.Property(s => s.TimetableTemplateId).HasColumnName("timetable_id");
        }
    }
}
