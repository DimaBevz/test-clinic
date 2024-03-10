using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class ApproveDocumentMessage
    {
        [Key]
        public Guid PhysicianDataId { get; set; }
        public PhysicianData PhysicianData { get; set; } = null!;
        public string Message { get; set; } = string.Empty;
    }
}
