using Application.Chat.DTOs.RequestDTOs;
using Application.Chat.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories
{
    public interface IChatRepository
    {
        public Task<MessageItemDto> AddChatHistoryAsync(AddMessageDto messageDto);

        public Task<GetChatMessagesDto> GetChatHistoryAsync(Guid sessionId);
    }
}
