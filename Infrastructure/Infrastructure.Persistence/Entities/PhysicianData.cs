using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class PhysicianData
    {
        [Key]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public DateOnly? Experience { get; set; }
        public float Rating { get; set; }
        public string? Bio { get; set; }

        public bool IsApproved { get; set; }

        public List<Position>? Positions { get; set; }
        public List<PhysicianSpecialty> PhysicianSpecialties { get; set; } = null!;

        public TimetableTemplate? TimetableTemplate { get; set; }
        public List<Comment> Comments { get; set; } = null!;

        //Need to discuss
        //public List<Education> Education { get; set; }  
    }
}
