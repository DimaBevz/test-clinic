using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

internal class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.ToTable("test_results");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.TestId).HasColumnName("test_id");
        builder.Property(x => x.PatientDataId).HasColumnName("patient_data_id");
        builder.Property(x => x.TestCriteriaId).HasColumnName("test_criteria_id");
        builder.Property(x => x.TotalScore).HasColumnName("total_score");
    }
}