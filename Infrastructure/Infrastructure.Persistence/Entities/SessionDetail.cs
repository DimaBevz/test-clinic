using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class SessionDetail
    {
        [Key]
        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public Guid? DiagnosisId { get; set; }
        public Diagnosis? Diagnosis { get; set; }

        public int CurrentPainScale { get; set; }
        public int AveragePainScaleLastMonth { get; set; }
        public int HighestPainScaleLastMonth { get; set; }

        public string Complaints { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;

        public IEnumerable<Document> Documents { get; set; }
    }
}
