using Application.Chat.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(MessageItemDto message);
    Task ReceiveConnectionStatus(string connectionStatus);
}