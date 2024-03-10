using Application.Call.DTOs;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class MeetingHistoryRepository : IMeetingHistoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MeetingHistoryRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<UpdatedMeetingDataDto?> UpdateHistoryMeetingAsync(MeetingDataDto meetingDataDto)
        {
            var currentSession = await _dbContext.Sessions
                .Include(s => s.MeetingHistory)
                .SingleAsync(s => s.MeetingId == meetingDataDto.Id);

            var meetingHistory = currentSession.MeetingHistory;

            if (meetingHistory is null)
            {
                return null;
            }

            meetingDataDto.ToEntity(meetingHistory);

            _dbContext.MeetingHistory.Update(meetingHistory);
            await _dbContext.SaveChangesAsync();

            var result = meetingHistory.ToDto();
            result = result with { MeetingId = meetingDataDto.Id };

            return result;
        }
    }
}
