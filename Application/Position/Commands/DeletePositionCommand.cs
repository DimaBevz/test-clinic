using Application.Common.Interfaces.Repositories;
using Application.Position.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Position.Commands
{
    public record DeletePositionCommand(Guid Id) : ICommand<PositionDto>;

    public class DeletePositionCommandHandler : ICommandHandler<DeletePositionCommand, PositionDto>
    {
        private readonly IPositionRepository _positionRepository;

        public DeletePositionCommandHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async ValueTask<PositionDto> Handle(DeletePositionCommand command, CancellationToken cancellationToken)
        {
            var result = await _positionRepository.RemovePositionAsync(command.Id);
            return result;
        }
    }
}
