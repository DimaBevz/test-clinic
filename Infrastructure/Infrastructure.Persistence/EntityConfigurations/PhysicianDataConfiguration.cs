using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PhysicianDataConfiguration : IEntityTypeConfiguration<PhysicianData>
    {
        public void Configure(EntityTypeBuilder<PhysicianData> builder)
        {
            builder.ToTable("physician_data");

            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Rating).HasColumnName("rating");
            builder.Property(p => p.Experience).HasColumnName("experience");
            builder.Property(p => p.IsApproved).HasColumnName("is_approved");
            builder.Property(p => p.Bio).HasColumnName("bio");

            builder.HasMany(pd => pd.Positions)
                .WithMany(p => p.PhysicianData)
                .UsingEntity<PhysicianSpecialty>(
                    j => j
                    .HasOne(ps => ps.Position)
                    .WithMany(p => p.PhysicianSpecialties)
                    .HasForeignKey(ps => ps.PositionId),
                    j => j
                    .HasOne(ps => ps.PhysicianData)
                    .WithMany(pd => pd.PhysicianSpecialties)
                    .HasForeignKey(ps => ps.PhysicianDataId),
                    j =>
                    {
                        j.HasKey(ps => new { ps.PhysicianDataId, ps.PositionId });
                        j.ToTable("physician_specialties");
                    }
                 );
        }
    }
}
