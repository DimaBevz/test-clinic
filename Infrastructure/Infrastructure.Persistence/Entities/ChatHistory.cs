using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class ChatHistory
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;
    }
}