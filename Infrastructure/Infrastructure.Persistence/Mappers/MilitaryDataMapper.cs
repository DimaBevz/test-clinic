using Application.Military.DTOs.Request;
using Application.Military.DTOs.Response;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class MilitaryDataMapper
    {
        public static partial MilitaryData ToEntity(this AddMilitaryDataDto source);
        public static partial GetMilitaryDataDto ToDto(this MilitaryData source);
    }
}
