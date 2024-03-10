using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class BugReportConfiguration : IEntityTypeConfiguration<BugReport>
{
    public void Configure(EntityTypeBuilder<BugReport> builder)
    {
        builder.ToTable("bug_reports");
        
        builder.Property(b=>b.Id).HasColumnName("id");
        builder.Property(b=>b.Topic).HasColumnName("topic");
        builder.Property(b=>b.Description).HasColumnName("description");
        builder.Property(b=>b.Status).HasColumnName("status");
        builder.Property(b => b.UserId).HasColumnName("user_id");
    }
}