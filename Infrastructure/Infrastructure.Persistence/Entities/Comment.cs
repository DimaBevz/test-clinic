namespace Infrastructure.Persistence.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CommentText { get; set; } = string.Empty;
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid PatientDataId { get; set; }
        public Guid PhysicianDataId { get; set; }

        public PatientData PatientData { get; set; } = null!;
        public PhysicianData PhysicianData { get; set; } = null!;
    }
}
