using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class UserPhotoData
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string PhotoObjectKey { get; set; } = string.Empty;
        public string PresignedUrl { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
