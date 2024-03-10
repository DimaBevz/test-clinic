using Application.Admin.DTOs.Request;
using Application.Admin.DTOs.Response;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.User.DTOs.ResponseDTOs;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IPhysicianRepository _physicianRepository;

        public UserService(IUserRepository userRepository, IFileService fileService, IPhysicianRepository physicianRepository) 
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _physicianRepository = physicianRepository;
        }

        public async Task<GetPartialUserDto> GetPartialUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserAsync(userId);

            var userPhoto = await _userRepository.GetUserPhotoAsync(userId);
            if (userPhoto?.ExpiresAt < DateTime.UtcNow)
            {
                var objectKey = userPhoto.ObjectKey;

                var resultDto = await _fileService.UpdatePresignedLinkAsync(objectKey!);
                userPhoto = await _userRepository.UpdateUserPhotoAsync(userId, resultDto);
            }

            user = user with { PhotoUrl = userPhoto?.PresignedUrl };

            return user;
        }

        public async Task<GetAllUsersDto> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync(cancellationToken);

            return users;
        }

        public async Task<GetUnapprovedPhysiciansDto> GetUnapprovedPhysiciansAsync(CancellationToken cancellationToken)
        {
            var physicians = await _physicianRepository.GetAllUnapprovedPhysiciansAsync(cancellationToken);

            return physicians;
        }

        public async Task<ApprovedPhysicianDto?> ApprovePhysicianAsync(ApprovePhysicianDto physicianDto, CancellationToken cancellationToken)
        {
            var result = await _physicianRepository.ApprovePhysicianAsync(physicianDto, cancellationToken);

            return result;
        }

        public async Task<DeclinedPhysicianDto?> DeclinePhysicianAsync(DeclinePhysicianDto physicianDto, CancellationToken cancellationToken)
        {
            var result = await _physicianRepository.DeclinePhysicianAsync(physicianDto, cancellationToken);

            return result;
        }

        public async Task<GetUserItemDto?> ChangeBanStatusAsync(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _userRepository.ChangeBanStatusAsync(userId, cancellationToken);

            return result;
        }
    }
}
