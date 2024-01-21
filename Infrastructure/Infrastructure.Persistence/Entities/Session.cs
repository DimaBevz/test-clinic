namespace Infrastructure.Persistence.Entities
{
    public class Session
    {
        public Guid Id { get; init; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? MeetingId { get; set; }

        public PhysicianData PhysicianData { get; set; } = null!;
        public Guid PhysicianDataId { get; set; }

        public PatientData PatientData { get; set; } = null!;
        public Guid PatientDataId { get; set; }

        public bool IsArchived { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public SessionDetail? SessionDetail { get; set; }

        public List<ChatHistory> ChatHistories { get; set; } = null!;
    }
}
