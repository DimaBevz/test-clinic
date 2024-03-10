using Application.Call.DTOs;
using Application.Common.Interfaces.Repositories;
using Mediator;

namespace Application.Call.Commands
{
    public record StartCallCommand(MeetingDataDto Dto) : ICommand<UpdatedMeetingDataDto>;

    public class StartCallCommandHandler : ICommandHandler<StartCallCommand, UpdatedMeetingDataDto>
    {
        private readonly IMeetingHistoryRepository _meetingHistoryRepository;

        public StartCallCommandHandler(IMeetingHistoryRepository meetingHistoryRepository)
        {
            _meetingHistoryRepository = meetingHistoryRepository;
        }

        public async ValueTask<UpdatedMeetingDataDto> Handle(StartCallCommand command, CancellationToken cancellationToken)
        {
            var result = await _meetingHistoryRepository.UpdateHistoryMeetingAsync(command.Dto);

            if (result is null) 
            {
                throw new ApplicationException("Couldn't find session by given meeting id");
            }

            return result;
        }
    }
}
