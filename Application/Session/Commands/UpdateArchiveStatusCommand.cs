using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Response;
using Mediator;

namespace Application.Session.Commands
{
    public record UpdateArchiveStatusCommand(Guid SessionId) : ICommand<SessionItemDto>;

    public class UpdateArchiveStatusCommandHandler : ICommandHandler<UpdateArchiveStatusCommand, SessionItemDto>
    {
        private readonly ISessionRepository _sessionRepository;

        public UpdateArchiveStatusCommandHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async ValueTask<SessionItemDto> Handle(UpdateArchiveStatusCommand command, CancellationToken cancellationToken)
        {
            var result = await _sessionRepository.UpdateArchiveStatusAsync(command.SessionId, cancellationToken);

            return result;
        }
    }
}
