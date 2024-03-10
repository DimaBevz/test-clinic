using Application.Call.DTOs;

namespace Application.Common.Interfaces.Repositories
{
    public interface IMeetingHistoryRepository
    {
        public Task<UpdatedMeetingDataDto?> UpdateHistoryMeetingAsync(MeetingDataDto meetingDataDto);
    }
}
