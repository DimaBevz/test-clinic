using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PhysicianSpecialtyConfiguration : IEntityTypeConfiguration<PhysicianSpecialty>
    {
        public void Configure(EntityTypeBuilder<PhysicianSpecialty> builder)
        {
            builder.ToTable("physician_specialties");

            builder.Property(ps => ps.PhysicianDataId).HasColumnName("physician_data_id");
            builder.Property(ps => ps.PositionId).HasColumnName("position_id");
        }
    }
}
