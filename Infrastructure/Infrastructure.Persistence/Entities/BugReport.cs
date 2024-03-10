using Application.Common.Enums;

namespace Infrastructure.Persistence.Entities;

public class BugReport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Topic { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BugReportStatus Status { get; set; }

    public List<BugReportPhoto>? BugReportPhotos { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}