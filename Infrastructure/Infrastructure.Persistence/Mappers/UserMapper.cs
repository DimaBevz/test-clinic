using Application.Common.DTOs;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class UserMapper
    {
        public static partial User ToEntity(this UpdateUserDto source);
        public static partial User ToEntity(this RegisterUserDto source);

        [MapProperty(nameof(@User.UserPhotoData.PresignedUrl), nameof(GetPartialUserDto.PhotoUrl))]
        public static partial GetPartialUserDto ToDto(this User source);

        [MapProperty(nameof(@FileDataDto.ObjectKey), nameof(UserPhotoData.PhotoObjectKey))]
        public static partial UserPhotoData ToUserPhotoData(this FileDataDto source);

        [MapProperty(nameof(@FileDataDto.ObjectKey), nameof(UserPhotoData.PhotoObjectKey))]
        public static partial void ToCurrentEntity(this FileDataDto source, UserPhotoData dest);
        public static partial void ToCurrentEntity(this UpdateUserDto source, User dest);

        [MapProperty(nameof(UserPhotoData.PhotoObjectKey), nameof(FileDataDto.ObjectKey))]
        [MapProperty(nameof(UserPhotoData.UserId), nameof(FileDataDto.Id))]
        public static partial FileDataDto ToDto(this UserPhotoData source);
    }
}
