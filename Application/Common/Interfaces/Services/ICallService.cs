using Application.Call.DTOs;

namespace Application.Common.Interfaces.Services;

public interface ICallService
{
    Task<string?> GetCallToken(CreateCallDto callInfo);
    Task<string?> CreateMeeting(DateTime sessionStartTime);
}
