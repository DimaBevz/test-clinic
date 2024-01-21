using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPhysicianRepository
    {
        Task<GetPhysicianDataDto> GetPhysicianDataAsync(Guid id);
        Task<GetPhysiciansDto> GetPhysiciansAsync();
        Task<GetPaginatedPhysiciansDto<PhysicianItemDto>> GetPhysiciansAsync(GetPhysiciansByParamsDto paramsDto);
        Task<GetPhysicianDataDto> AddPhysicianDataAsync(AddPhysicianDto physicianDto);
        Task<GetPhysicianDataDto> UpdatePhysicianAsync(UpdatePhysicianDto updatePhysicianDto);
    }
}
