using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class TimetableTemplateConfiguration : IEntityTypeConfiguration<TimetableTemplate>
    {
        public void Configure(EntityTypeBuilder<TimetableTemplate> builder)
        {
            builder.ToTable("timetable_templates");

            builder.Property(t => t.Id).HasColumnName("timetable_id");
            builder.Property(t => t.StartDate).HasColumnName("start_date");
            builder.Property(t => t.EndDate).HasColumnName("end_date");
            builder.Property(t => t.PhysicianDataId).HasColumnName("physician_data_id");
        }
    }
}
