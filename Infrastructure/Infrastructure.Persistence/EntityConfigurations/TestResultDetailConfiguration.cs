using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

internal class TestResultDetailConfiguration : IEntityTypeConfiguration<TestResultDetail>
{
    public void Configure(EntityTypeBuilder<TestResultDetail> builder)
    {
        builder.ToTable("test_result_details");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.TestQuestionId).HasColumnName("test_question_id");
        builder.Property(x => x.TestOptionId).HasColumnName("test_option_id");
        builder.Property(x => x.TestResultId).HasColumnName("test_result_id");
    }
}