using Application.Common.Interfaces.Repositories;
using Application.Position.DTOs.RequestDTOs;
using Application.Position.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Position.Commands
{
    public record AddPositionRangeCommand(AddPositionListDto Dto) : ICommand<GetPositionsDto>;

    public class AddPositionRangeCommandHandler : ICommandHandler<AddPositionRangeCommand, GetPositionsDto>
    {
        private readonly IPositionRepository _positionRepository;

        public AddPositionRangeCommandHandler(IPositionRepository positionRepository) 
        {
            _positionRepository = positionRepository;
        }

        public async ValueTask<GetPositionsDto> Handle(AddPositionRangeCommand command, CancellationToken cancellationToken)
        {
            var result = await _positionRepository.AddPositionRangeAsync(command.Dto);
            return result;
        }
    }
}
