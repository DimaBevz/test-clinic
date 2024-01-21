using Application.Chat.DTOs.RequestDTOs;
using Application.Chat.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Mediator;

namespace Application.Chat.Commands
{
    public record AddMessageCommand(AddMessageDto Dto) : ICommand<MessageItemDto>;

    public class AddMessageCommandHandler : ICommandHandler<AddMessageCommand, MessageItemDto>
    {
        private readonly IChatRepository _chatRepository;

        public AddMessageCommandHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async ValueTask<MessageItemDto> Handle(AddMessageCommand command, CancellationToken cancellationToken)
        {
            var result = await _chatRepository.AddChatHistoryAsync(command.Dto);
            return result;
        }
    }
}
