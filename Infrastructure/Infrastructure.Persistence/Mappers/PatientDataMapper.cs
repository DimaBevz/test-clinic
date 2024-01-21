using Application.Patient.DTOs.RequestDTOs;
using Application.Patient.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class PatientDataMapper
    {
        [MapProperty(nameof(AddPatientDto.Id), nameof(PatientData.UserId))]
        public static partial PatientData ToEntity(this AddPatientDto source);
        [MapProperty(nameof(UpdatePatientDto.Id), nameof(PatientData.UserId))]
        public static partial PatientData ToEntity(this UpdatePatientDto source);

        [MapProperty(nameof(PatientData.UserId), nameof(GetPatientDataDto.Id))]
        public static partial GetPatientDataDto ToDto(this PatientData source);

        public static partial void ToCurrentEntity(this UpdatePatientDto source, PatientData dest);
    }
}
