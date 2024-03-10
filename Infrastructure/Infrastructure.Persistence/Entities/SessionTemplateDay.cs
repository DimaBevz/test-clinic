using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class SessionTemplateDay
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsActive { get; set; }

        public Guid TimetableTemplateId { get; set; }

        public IEnumerable<SessionTemplateTimes> SessionTemplateTimes { get; set; } = null!;
    }
}
