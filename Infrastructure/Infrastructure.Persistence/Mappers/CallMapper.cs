using Application.Call.DTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers
{
    [Mapper]
    internal static partial class CallMapper
    {
        public static partial void ToEntity(this MeetingDataDto source, MeetingHistory dest);
        public static partial UpdatedMeetingDataDto ToDto(this MeetingHistory source);
    }
}
