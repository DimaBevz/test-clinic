using Application.Chat.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Mediator;

namespace Application.Chat.Queries
{
    public record GetSessionMessagesQuery(Guid SessionId) : IQuery<GetChatMessagesDto>;

    public class GetSessionMessagesQueryHandler : IQueryHandler<GetSessionMessagesQuery, GetChatMessagesDto>
    {
        private readonly IChatRepository _chatRepository;

        public GetSessionMessagesQueryHandler(IChatRepository chatRepository) 
        { 
            _chatRepository = chatRepository;
        }

        public async ValueTask<GetChatMessagesDto> Handle(GetSessionMessagesQuery query, CancellationToken cancellationToken)
        {
            var result = await _chatRepository.GetChatHistoryAsync(query.SessionId);
            return result;
        }
    }
}
