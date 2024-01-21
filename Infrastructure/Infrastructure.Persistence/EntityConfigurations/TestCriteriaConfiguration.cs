using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class TestCriteriaConfiguration : IEntityTypeConfiguration<TestCriteria>
{
    public void Configure(EntityTypeBuilder<TestCriteria> builder)
    {
        builder.ToTable("test_criteria");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.TestId).HasColumnName("test_id");
        builder.Property(x => x.Min).HasColumnName("min");
        builder.Property(x => x.Max).HasColumnName("max");
        builder.Property(x => x.Verdict).HasColumnName("verdict");
    }
}