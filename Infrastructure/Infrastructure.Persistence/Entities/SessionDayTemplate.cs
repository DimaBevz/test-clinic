using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class SessionDayTemplate
    {
        [Key]
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsActive { get; set; }
        public Guid TimetableTemplateId { get; set; }

        public IEnumerable<SessionTemplate> SessionTemplates { get; set; }
    }
}
