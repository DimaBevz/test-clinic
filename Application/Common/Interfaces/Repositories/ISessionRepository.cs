using Application.Session.DTOs;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;

namespace Application.Common.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<AddedSessionDto> AddSessionAsync(AddSessionDto request, Guid patientId, Guid meetingId, CancellationToken cancellationToken);
        Task<GetSessionsResponseDto> GetSessionsAsync(GetSessionsRequestDto request, CancellationToken cancellationToken);
        Task<GetPaginatedSessionsDto> GetSessionsByParamsAsync(GetSessionsByParamsDto request, CancellationToken cancellationToken);
        Task<SessionDetailsDto> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellationToken);
        Task<Guid?> GetMeetingIdAsync(Guid sessionId, CancellationToken cancellationToken);
        Task<SessionDetailsDto> UpdateSessionAsync(UpdateSessionRequestDto request, CancellationToken cancellationToken);
        Task<SessionItemDto> UpdateArchiveStatusAsync(Guid sessionId, CancellationToken  cancellationToken);
        Task<SessionItemDto> DeleteSessionAsync(Guid sessionId, CancellationToken cancellationToken);
    }
}
