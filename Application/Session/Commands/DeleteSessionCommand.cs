using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Response;
using Mediator;

namespace Application.Session.Commands
{
    public record DeleteSessionCommand(Guid SessionId) : ICommand<SessionItemDto>;

    public class DeleteSessionCommandHandler : ICommandHandler<DeleteSessionCommand, SessionItemDto>
    {
        private readonly ISessionRepository _sessionRepository;

        public DeleteSessionCommandHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async ValueTask<SessionItemDto> Handle(DeleteSessionCommand command, CancellationToken cancellationToken)
        {
            var result = await _sessionRepository.DeleteSessionAsync(command.SessionId, cancellationToken);

            return result;
        }
    }
}
