using Application.Common.Interfaces.Repositories;
using Application.Position.DTOs.RequestDTOs;
using Application.Position.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Position.Commands
{
    public record UpdatePositionCommand(UpdatePositionDto Dto) : ICommand<PositionDto>;

    public class UpdatePositionCommandHandler : ICommandHandler<UpdatePositionCommand, PositionDto>
    {
        private readonly IPositionRepository _positionRepository;

        public UpdatePositionCommandHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async ValueTask<PositionDto> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
        {
            var result = await _positionRepository.UpdatePositionAsync(command.Dto);
            return result;
        }
    }
}
