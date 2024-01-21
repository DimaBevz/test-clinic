using Application.Session.DTOs.Request;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class SessionMapper
    {
        [MapProperty(nameof(UpdateSessionRequestDto.SessionId), nameof(Session.Id))]
        [MapProperty(nameof(UpdateSessionRequestDto.PatientId), nameof(Session.PatientDataId))]
        public static partial Session ToSession(this UpdateSessionRequestDto request);
        public static partial SessionDetail ToUpdatedSessionDetails(this UpdateSessionRequestDto request);

        [MapProperty(nameof(UpdateSessionRequestDto.DiagnosisTitle), nameof(Diagnosis.Title))]
        public static partial Diagnosis ToDiagnosis(this UpdateSessionRequestDto request);
    }
}
