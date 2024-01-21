using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPartialUserDto> GetUserAsync(Guid id)
    {
        var user = await _dbContext.Users.SingleAsync(u => u.Id == id);

        var dto = user.ToDto();
        return dto;
    }

    public async Task<GetPartialUserDto> AddUserAsync(Guid userId, RegisterUserDto registerUserDto)
    {
        var user = registerUserDto.ToEntity();
        user.Id = userId;

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var dto = user.ToDto();
        return dto;
    }

    public async Task<FileDataDto?> UpdateUserPhotoAsync(Guid userId, FileDataDto dto)
    {
        FileDataDto? photoDataDto = null;
        var user = await _dbContext.Users
            .Include(u => u.UserPhotoData)
            .SingleOrDefaultAsync(u => u.Id == userId);

        if (user is not null)
        {
            var photoData = dto.ToUserPhotoData();
            user.UserPhotoData = photoData;

            await _dbContext.SaveChangesAsync();

            photoDataDto = photoData.ToDto();
        }

        return photoDataDto;
    }

    public async Task<FileDataDto?> GetUserPhotoAsync(Guid userId)
    {
        var photoData = await _dbContext.UserPhotoData
            .SingleOrDefaultAsync(p => p.UserId == userId);

        return photoData?.ToDto();
    }

    public async Task<GetPartialUserDto> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var currentEntityToUpdate = await _dbContext.Users
            .Include(u => u.UserPhotoData)
            .SingleAsync(u => u.Id == updateUserDto.Id);

        updateUserDto.ToCurrentEntity(currentEntityToUpdate);

        _dbContext.Users.Update(currentEntityToUpdate);
        await _dbContext.SaveChangesAsync();

        var dto = currentEntityToUpdate.ToDto();
        return dto;
    }
}