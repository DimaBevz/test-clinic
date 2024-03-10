namespace Application.Common.DTOs
{
    public class FileDataDto
    {
        public Guid Id { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string PresignedUrl { get; set; } = string.Empty;
        public string? ContentType { get; set; }
        public string? ObjectKey { get; set; }
    }
}
