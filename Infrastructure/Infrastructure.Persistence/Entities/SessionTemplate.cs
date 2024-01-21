using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class SessionTemplate
    {
        [Key]
        public Guid Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid SessionDayTemplateId { get; set; }
        public SessionDayTemplate SessionDayTemplate { get; set; }
    }
}
