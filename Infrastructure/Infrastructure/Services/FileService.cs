using Amazon.S3.Model;
using Amazon.S3;
using Application.Common.Interfaces.Services;
using Application.Common.Options;
using Microsoft.Extensions.Options;
using System.Net;
using Application.Common.Enums;
using Application.Common.DTOs;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IAmazonS3 _s3Service;
        private readonly S3BucketOptions _s3Options;

        public FileService(IAmazonS3 s3Service, IOptions<S3BucketOptions> s3Options)
        {
            _s3Service = s3Service;
            _s3Options = s3Options.Value;
        }

        private async Task<string> GetPresignedFileLinkAsync(string objectKey, DateTime expiresAt)
        {
            var getUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _s3Options.Name,
                Key = objectKey,
                Expires = expiresAt
            };

            var presignLink = await _s3Service.GetPreSignedURLAsync(getUrlRequest);

            return presignLink;
        }

        public async Task<FileDataDto> UpdatePresignedLinkAsync(string fileObjectKey)
        {
            var linkTimeExparation = DateTime.UtcNow.AddDays(1);
            var presignedLink = await GetPresignedFileLinkAsync(fileObjectKey, linkTimeExparation);

            var fileDataDto = new FileDataDto
            {
                ExpiresAt = linkTimeExparation,
                PresignedUrl = presignedLink
            };

            return fileDataDto;
        }

        public async Task DeleteFileAsync(string objectKey)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _s3Options.Name,
                Key = objectKey
            };

            await _s3Service.DeleteObjectAsync(deleteObjectRequest);
        }

        public async Task<FileDataDto?> UploadFileAsync(Stream docStream, string contentType, FolderName folderName, string fileName)
        {
            FileDataDto? fileDataDto = null;
            var folderKeyName = Enum.GetName(folderName);
            
            var objectKey = $"{_s3Options.Folders[folderKeyName!]}{fileName}";

            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _s3Options.Name,
                Key = objectKey,
                InputStream = docStream,
                ContentType = contentType
            };

            var response = await _s3Service.PutObjectAsync(putObjectRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                var linkTimeExparation = DateTime.UtcNow.AddDays(1);
                var presignedLink = await GetPresignedFileLinkAsync(objectKey, linkTimeExparation);

                fileDataDto = new FileDataDto
                {
                    ContentType = contentType,
                    ExpiresAt = linkTimeExparation,
                    PresignedUrl = presignedLink,
                    ObjectKey = objectKey
                };
            }

            return fileDataDto;
        }
    }
}
