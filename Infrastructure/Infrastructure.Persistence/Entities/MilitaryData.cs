using Application.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class MilitaryData
    {
        [Key]
        public Guid PatientDataId { get; set; }
        public PatientData? PatientData { get; set; } = null!;
        public bool IsVeteran { get; set; }
        public string Specialty { get; set; } = string.Empty;
        public string ServicePlace { get; set; } = string.Empty;
        public bool IsOnVacation { get; set; }
        public bool HasDisability { get; set; }
        public DisabilityCategory? DisabilityCategory { get; set; }
        public string HealthProblems { get; set; } = string.Empty;
        public bool NeedMedicalOrPsychoCare { get; set; }
        public bool HasDocuments { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string RehabilitationAndSupportNeeds { get; set; } = string.Empty;
        public bool HasFamilyInNeed { get; set; }
        public string HowLearnedAboutRehabCenter { get; set; } = string.Empty;
        public bool WasRehabilitated { get; set; }
        public string? PlaceOfRehabilitation { get; set; }
        public string? ResultOfRehabilitation { get; set; }
    }
}
