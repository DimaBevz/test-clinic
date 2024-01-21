using Application.Common.DTOs;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<GetPartialUserDto> GetUserAsync(Guid id);
    Task<GetPartialUserDto> AddUserAsync(Guid userId, RegisterUserDto registerUserDto);
    Task<GetPartialUserDto> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<FileDataDto?> UpdateUserPhotoAsync(Guid userId, FileDataDto dto);
    Task<FileDataDto?> GetUserPhotoAsync(Guid userId);
}