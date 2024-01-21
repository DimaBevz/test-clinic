using Application.Chat.DTOs.RequestDTOs;
using Application.Chat.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatRepository(ApplicationDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<MessageItemDto> AddChatHistoryAsync(AddMessageDto messageDto)
        {
            var message = messageDto.ToEntity();

            await _dbContext.ChatHistories.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            message = await _dbContext.ChatHistories.Include(c => c.User).SingleAsync(c => c.Id == message.Id);
            var dto = message.ToMessageItemDto();
            return dto;
        }

        public async Task<GetChatMessagesDto> GetChatHistoryAsync(Guid sessionId)
        {
            var messages = await _dbContext.ChatHistories
                .Include(c => c.User)
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();

            var messageItemDtos = messages.Select(c => c.ToMessageItemDto()).ToList();
            var resultDto = new GetChatMessagesDto(messageItemDtos);

            return resultDto;
        }
    }
}
