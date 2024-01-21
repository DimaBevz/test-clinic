using Application.Position.DTOs.RequestDTOs;
using Application.Position.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class PositionMapper
    {
        public static partial PositionDto ToDto(this Position source);

        public static partial Position ToEntity(this AddPositionDto source);
        public static partial PositionDto ToEntity(this UpdatePositionDto source);

        public static partial void ToCurrentEntity(this UpdatePositionDto source, Position dest);
    }
}
