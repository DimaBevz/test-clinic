using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Guid?> GetMeetingId(Guid sessionId, CancellationToken cancellationToken)
        {
            return await _sessionRepository.GetMeetingIdAsync(sessionId, cancellationToken);
        }
    }
}
