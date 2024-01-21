using Application.Extensions;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class PhysicianDataMapper
    {
        [MapProperty(nameof(AddPhysicianDto.Id), nameof(PhysicianData.UserId))]
        public static partial PhysicianData ToEntity(this AddPhysicianDto source);

        public static GetPhysicianDataDto ToDto(this PhysicianData source)
        {
            var getPhysicianDataDto = MapPhysicianDataToGetPhysicianDataDto(source);

            if (source.Experience is not null)
            {
                var experience = source.Experience.Value;
                var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

                var experienceInYears = currentDate.GetSubstractionInYears(experience);

                getPhysicianDataDto = getPhysicianDataDto with { Experience = experienceInYears };
            }

            if (source.Positions is not null)
            {
                var specialtyDtos = source.Positions.Select(p => p.ToDto()).ToList();
                getPhysicianDataDto = getPhysicianDataDto with { Positions = specialtyDtos };
            }

            return getPhysicianDataDto;
        }

        [MapperIgnoreSource(nameof(PhysicianData.Experience))]
        public static partial GetPhysicianDataDto MapPhysicianDataToGetPhysicianDataDto(this PhysicianData source);

        [MapProperty(nameof(@PhysicianData.UserId), nameof(PhysicianItemDto.Id))]
        [MapProperty(nameof(@PhysicianData.User.FirstName), nameof(PhysicianItemDto.FirstName))]
        [MapProperty(nameof(@PhysicianData.User.LastName), nameof(PhysicianItemDto.LastName))]
        [MapProperty(nameof(@PhysicianData.User.Patronymic), nameof(PhysicianItemDto.Patronymic))]
        [MapProperty(nameof(@PhysicianData.User.UserPhotoData.PresignedUrl), nameof(PhysicianItemDto.PhotoUrl))]
        [MapperIgnoreSource(nameof(PhysicianData.Experience))]
        public static partial PhysicianItemDto MapPhysicianDataToPhysicianItemDto(this PhysicianData source);

        public static PhysicianItemDto ToItemDto(this PhysicianData source)
        {
            var physicianItemDto = MapPhysicianDataToPhysicianItemDto(source);
            physicianItemDto = physicianItemDto with { CommentsCount = source.Comments.Count };

            if (source.Experience is not null)
            {
                var experience = source.Experience.Value;
                var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

                var experienceInYears = currentDate.GetSubstractionInYears(experience);

                physicianItemDto = physicianItemDto with { Experience = experienceInYears };
            }

            if (source.Positions is not null)
            {
                var specialtyDtos = source.Positions.Select(p => p.ToDto()).ToList();
                physicianItemDto = physicianItemDto with { Positions = specialtyDtos };
            }

            return physicianItemDto;
        }

        public static partial void ToCurrentEntity(this UpdatePhysicianDto source, PhysicianData dest);
    }
}
