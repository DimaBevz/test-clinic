using Application.Common.Enums;

namespace Application.Military.DTOs.Request
{
    public record AddMilitaryDataDto
    (
        bool IsVeteran,
        string Specialty,
        string ServicePlace,
        bool IsOnVacation,
        bool HasDisability,
        DisabilityCategory? DisabilityCategory,
        string HealthProblems,
        bool NeedMedicalOrPsychoCare,
        bool HasDocuments,
        string DocumentNumber,
        string RehabilitationAndSupportNeeds,
        bool HasFamilyInNeed,
        string HowLearnedAboutRehabCenter,
        bool WasRehabilitated,
        string? PlaceOfRehabilitation,
        string? ResultOfRehabilitation
    );
}
