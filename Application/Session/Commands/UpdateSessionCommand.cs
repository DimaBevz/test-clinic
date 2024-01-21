using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;
using Mediator;

namespace Application.Session.Commands
{
    public record UpdateSessionCommand(UpdateSessionRequestDto Request) : ICommand<SessionDetailsDto>;

    public class UpdateSessionCommandHandler : ICommandHandler<UpdateSessionCommand, SessionDetailsDto>
    {
        private readonly ISessionRepository _sessionRepository;

        public UpdateSessionCommandHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async ValueTask<SessionDetailsDto> Handle(UpdateSessionCommand command, CancellationToken cancellationToken)
        {
            var result = await _sessionRepository.UpdateSessionAsync(command.Request, cancellationToken);

            return result;
        }
    }
}
