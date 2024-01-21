namespace Application.Common.Interfaces.Services
{
    public interface ISessionService
    {
        Task<Guid?> GetMeetingId(Guid sessionId, CancellationToken cancellationToken);
    }
}
