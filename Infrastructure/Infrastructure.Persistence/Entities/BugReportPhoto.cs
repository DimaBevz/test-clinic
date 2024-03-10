namespace Infrastructure.Persistence.Entities;

public class BugReportPhoto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string PhotoObjectKey { get; set; } = string.Empty;
    public string PresignedUrl { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    
    public Guid BugReportId { get; set; }
    public BugReport BugReport { get; set; } = null!;
}