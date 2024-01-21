using Application.Common.Enums;

namespace Infrastructure.Persistence.Entities
{
    public class Document
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public DocumentType Type { get; set; }

        public string DocumentObjectKey { get; set; } = string.Empty;
        public string PresignedUrl { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid? SessionDetailSessionId { get; set; }
    }
}
