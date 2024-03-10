using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class TimetableTemplate
    {
        [Key]
        public Guid Id { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}

        public Guid PhysicianDataId { get; set; }
        public PhysicianData PhysicianData { get; set; } = null!;

        public IEnumerable<SessionTemplateDay> SessionTemplateDays { get; set; } = null!;
    }
}
