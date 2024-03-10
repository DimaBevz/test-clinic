using Application.Admin.DTOs.Request;
using Application.Admin.DTOs.Response;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPhysicianRepository
    {
        Task<GetPhysicianDataDto> GetPhysicianDataAsync(Guid id);
        Task<GetPhysiciansDto> GetPhysiciansAsync();
        Task<GetUnapprovedPhysiciansDto> GetAllUnapprovedPhysiciansAsync(CancellationToken cancellationToken);
        Task<GetPaginatedPhysiciansDto<PhysicianItemDto>> GetPhysiciansAsync(GetPhysiciansByParamsDto paramsDto);
        Task<GetPhysicianDataDto> AddPhysicianDataAsync(AddPhysicianDto physicianDto);
        Task<GetPhysicianDataDto> UpdatePhysicianAsync(UpdatePhysicianDto updatePhysicianDto);
        Task<ApprovedPhysicianDto?> ApprovePhysicianAsync(ApprovePhysicianDto physicianDto,
            CancellationToken cancellationToken);
        Task<DeclinedPhysicianDto?> DeclinePhysicianAsync(DeclinePhysicianDto physicianDto,
            CancellationToken cancellationToken);
    }
}
