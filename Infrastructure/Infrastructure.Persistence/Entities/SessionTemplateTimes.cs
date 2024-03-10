using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class SessionTemplateTimes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Guid SessionTemplateDayId { get; set; }
        public SessionTemplateDay SessionTemplateDay { get; set; } = null!;
    }
}
