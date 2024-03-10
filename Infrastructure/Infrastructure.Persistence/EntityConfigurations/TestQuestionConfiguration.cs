using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

internal class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.ToTable("test_questions");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.TestId).HasColumnName("test_id");
        builder.Property(x => x.Text).HasColumnName("text");
    }
}