namespace Application.Common.Interfaces.Hubs;

public interface ICallClient
{
    Task SendCallId(string callId);
}
