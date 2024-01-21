using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.User.Commands
{
    public record UploadUserPhotoCommand(Stream FileStream, string ContentType) : ICommand<FileDataDto>;

    public sealed class UploadUserPhotoCommandHandler : ICommandHandler<UploadUserPhotoCommand, FileDataDto>
    {
        private readonly IFileService _fileService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public UploadUserPhotoCommandHandler(IFileService fileService, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _fileService = fileService;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async ValueTask<FileDataDto> Handle(UploadUserPhotoCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_currentUserService.UserId);

            if (!command.ContentType.StartsWith("image/"))
            {
                throw new ApplicationException("Uploaded file is not of type image"); 
            }

            await DeleteExistingUserPhotoAsync(userId);

            var uploadedPhoto = await UploadPhotoAsync(command.FileStream, command.ContentType);
            var result = await UpdateUserPhotoDataAsync(userId, uploadedPhoto);

            return result;
        }

        private async Task DeleteExistingUserPhotoAsync(Guid userId)
        {
            var photoDataDto = await _userRepository.GetUserPhotoAsync(userId);
            if (photoDataDto is not null)
            {
                await _fileService.DeleteFileAsync(photoDataDto.ObjectKey);
            }
        }

        private async Task<FileDataDto> UploadPhotoAsync(Stream fileStream, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}";
            var uploadResult = await _fileService.UploadFileAsync(fileStream, contentType, FolderName.ProfilePhotos, fileName);
            if (uploadResult is null)
            {
                throw new ApplicationException("Photo hasn't been uploaded on S3 bucket");
            }

            return uploadResult;
        }

        private async Task<FileDataDto> UpdateUserPhotoDataAsync(Guid userId, FileDataDto photoData)
        {
            var fileDataDto = await _userRepository.UpdateUserPhotoAsync(userId, photoData);
            if (fileDataDto is null)
            {
                await _fileService.DeleteFileAsync(photoData.ObjectKey);
                throw new ApplicationException("Photo data hasn't been updated");
            }

            return fileDataDto;
        }
    }
}
