using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class MeetingHistory
    {
        [Key]
        public Guid SessionId { get; set; }
        public Session? Session { get; set; }

        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
