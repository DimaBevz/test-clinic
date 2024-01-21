using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Response;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Session.Queries
{
    public record GetSessionByIdQuery(Guid SessionId) : IRequest<SessionDetailsDto>;

    public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, SessionDetailsDto>
    {
        private readonly ISessionRepository _sessionRepository;
        private const string ExceptionMessage = "No such appointment with such Id";

        public GetSessionByIdQueryHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async ValueTask<SessionDetailsDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _sessionRepository.GetSessionByIdAsync(request.SessionId, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException(ExceptionMessage);
            }

            return result;
        }
    }
}
