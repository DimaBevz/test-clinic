using Application.Common.Interfaces.Repositories;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Physician.Commands
{
    public record UpdatePhysicianCommand(UpdatePhysicianDto Dto) : ICommand<GetPhysicianDataDto>;

    public class UpdatePhysicianCommandHandler : ICommandHandler<UpdatePhysicianCommand, GetPhysicianDataDto>
    {
        private readonly IPhysicianRepository _repository;

        public UpdatePhysicianCommandHandler(IPhysicianRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPhysicianDataDto> Handle(UpdatePhysicianCommand command, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdatePhysicianAsync(command.Dto);
            return result;
        }
    }
}
