using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.User.DTOs.ResponseDTOs;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;

        public UserService(IUserRepository userRepository, IFileService fileService) 
        {
            _userRepository = userRepository;
            _fileService = fileService;
        }

        public async Task<GetPartialUserDto> GetPartialUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserAsync(userId);

            var userPhoto = await _userRepository.GetUserPhotoAsync(userId);
            if (userPhoto?.ExpiresAt < DateTime.UtcNow)
            {
                var objectKey = userPhoto.ObjectKey;

                var resultDto = await _fileService.UpdatePresignedLinkAsync(objectKey);
                userPhoto = await _userRepository.UpdateUserPhotoAsync(userId, resultDto);
            }

            user = user with { PhotoUrl = userPhoto?.PresignedUrl };

            return user;
        }
    }
}
