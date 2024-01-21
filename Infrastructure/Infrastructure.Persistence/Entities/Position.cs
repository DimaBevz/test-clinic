namespace Infrastructure.Persistence.Entities
{
    public class Position
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Specialty { get; set; } = string.Empty;

        public List<PhysicianData> PhysicianData { get; set; } = null!;
        public List<PhysicianSpecialty>? PhysicianSpecialties { get; set; } = null!;
    }
}
