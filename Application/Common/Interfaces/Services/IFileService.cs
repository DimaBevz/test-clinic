using Application.Common.DTOs;
using Application.Common.Enums;

namespace Application.Common.Interfaces.Services
{
    public interface IFileService
    {
        public Task<FileDataDto> UpdatePresignedLinkAsync(string fileObjectKey);
        public Task<FileDataDto> UploadFileAsync(
            Stream fileStream, 
            string contentType, 
            FolderName folderName, 
            string fileName);
        public Task DeleteFileAsync(string objectKey);
    }
}
