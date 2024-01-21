using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class SessionTemplateConfiguration : IEntityTypeConfiguration<SessionTemplate>
    {
        public void Configure(EntityTypeBuilder<SessionTemplate> builder)
        {
            builder.ToTable("session_templates");

            builder.Property(s => s.Id).HasColumnName("session_template_id");
            builder.Property(s => s.StartTime).HasColumnName("start_time");
            builder.Property(s => s.EndTime).HasColumnName("end_time");
            builder.Property(s => s.SessionDayTemplateId).HasColumnName("session_day_template_id");
        }
    }
}
