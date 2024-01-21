using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PatientDataConfiguration : IEntityTypeConfiguration<PatientData>
    {
        public void Configure(EntityTypeBuilder<PatientData> builder)
        {
            builder.ToTable("patient_data");

            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Settlement).HasColumnName("settlement");
            builder.Property(p => p.Apartment).HasColumnName("apartment");
            builder.Property(p => p.Street).HasColumnName("street");
            builder.Property(p => p.House).HasColumnName("house");
            builder.Property(p => p.Institution).HasColumnName("institution");
            builder.Property(p => p.Position).HasColumnName("position");
        }
    }
}
