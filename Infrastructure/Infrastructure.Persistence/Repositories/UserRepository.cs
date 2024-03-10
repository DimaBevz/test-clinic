using Application.Admin.DTOs.Response;
using Application.Common.DTOs;
using Application.Common.Enums;
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

    public async Task<GetAllUsersDto> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Include(u => u.UserPhotoData)
            .Where(u => u.Role != RoleType.Admin)
            .Select(u => new GetUserItemDto(u.Id, u.LastName, u.FirstName, u.Patronymic,
                u.UserPhotoData.PresignedUrl ?? "", u.Role, u.IsBanned))
            .ToListAsync(cancellationToken);

        var result = new GetAllUsersDto(users);
        return result;
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
        user.IsBanned = false;

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
            .SingleOrDefaultAsync(photoData => photoData.Id == userId);

        if (user is not null)
        {
            if (user.UserPhotoData is null)
            {
                var userPhotoData = dto.ToUserPhotoData();
                user.UserPhotoData = userPhotoData;
            }
            else
            {
                dto.ToCurrentEntity(user.UserPhotoData);
            }

            await _dbContext.SaveChangesAsync();
            photoDataDto = user.UserPhotoData.ToDto();
        }

        return photoDataDto;
    }

    public async Task<FileDataDto?> GetUserPhotoAsync(Guid userId)
    {
        var photoData = await _dbContext.UserPhotoData
            .SingleOrDefaultAsync(p => p.UserId == userId);

        return photoData?.ToDto();
    }

    public async Task<GetUserItemDto?> ChangeBanStatusAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserPhotoData)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
        {
            return null;
        }

        user.IsBanned = !user.IsBanned;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = new GetUserItemDto(user.Id, user.LastName, user.FirstName, user.Patronymic,
            user.UserPhotoData?.PresignedUrl ?? "", user.Role, user.IsBanned);

        return result;
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

    public async Task<bool> RemoveUserAsync(Guid userId)
    {
        var userEntityToDelete = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);

        if (userEntityToDelete is not null) 
        {
            _dbContext.Users.Remove(userEntityToDelete);
        }
        
        var deletedItemsCount = await _dbContext.SaveChangesAsync(true);
        return deletedItemsCount > 0;
    }
}