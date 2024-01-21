using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

internal class TestOptionConfiguration : IEntityTypeConfiguration<TestOption>
{
    public void Configure(EntityTypeBuilder<TestOption> builder)
    {
        builder.ToTable("test_options");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Text).HasColumnName("text");
        builder.Property(x => x.TestQuestionId).HasColumnName("test_question_id");
        builder.Property(x => x.Points).HasColumnName("points");
    }
}