using Application.Admin.DTOs.Request;
using Application.Admin.DTOs.Response;
using Application.User.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Services;

public interface IUserService
{
    public Task<GetPartialUserDto> GetPartialUserAsync(Guid userId);
    public Task<GetAllUsersDto> GetAllUsersAsync(CancellationToken cancellationToken);
    public Task<GetUnapprovedPhysiciansDto> GetUnapprovedPhysiciansAsync(CancellationToken cancellationToken);
    public Task<ApprovedPhysicianDto?> ApprovePhysicianAsync(ApprovePhysicianDto physicianDto,
        CancellationToken cancellationToken);
    public Task<DeclinedPhysicianDto?> DeclinePhysicianAsync(DeclinePhysicianDto physicianDto,
        CancellationToken cancellationToken);
    public Task<GetUserItemDto?> ChangeBanStatusAsync(Guid userId, CancellationToken cancellationToken);
}
