using Application.User.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Services;

public interface IUserService
{
    public Task<GetPartialUserDto> GetPartialUserAsync(Guid userId);
}
