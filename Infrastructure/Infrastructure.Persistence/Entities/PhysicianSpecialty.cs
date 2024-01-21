using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Entities
{
    public class PhysicianSpecialty
    {
        public Guid PhysicianDataId { get; set; }
        public PhysicianData PhysicianData { get; set; } = null!;

        public Guid PositionId { get; set; }
        public Position Position { get; set; } = null!;
    }
}
