using Application.Call.DTOs;
using Application.Common.Interfaces.Repositories;
using Mediator;
namespace Application.Call.Commands
{
    public record EndCallCommand(MeetingDataDto Dto) : ICommand<UpdatedMeetingDataDto>;

    public class EndCallCommandHandler : ICommandHandler<EndCallCommand, UpdatedMeetingDataDto>
    {
        private readonly IMeetingHistoryRepository _meetingHistoryRepository;
        private readonly ISessionRepository _sessionRepository;

        public EndCallCommandHandler(IMeetingHistoryRepository meetingHistoryRepository, ISessionRepository sessionRepository)
        {
            _meetingHistoryRepository = meetingHistoryRepository;
            _sessionRepository = sessionRepository;
        }

        public async ValueTask<UpdatedMeetingDataDto> Handle(EndCallCommand command, CancellationToken cancellationToken)
        {
            var result = await _meetingHistoryRepository.UpdateHistoryMeetingAsync(command.Dto);

            if (result is null)
            {
                throw new ApplicationException("Couldn't find session by given meeting id");
            }

            await _sessionRepository.UpdateArchiveStatusAsync(result.SessionId, cancellationToken);

            return result;
        }
    }
}
