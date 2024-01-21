using Application.Patient.DTOs.RequestDTOs;
using Application.Patient.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        Task<GetPatientDataDto> GetPatientDataAsync(Guid id);
        Task<GetPatientDataDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto);
        Task<GetPatientDataDto> AddPatientDataAsync(AddPatientDto patientDto);
    }
}
