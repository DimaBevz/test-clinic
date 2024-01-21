using Application.Common.Interfaces.Repositories;
using Application.Patient.DTOs.RequestDTOs;
using Application.Patient.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Patient.Commands
{
    public record UpdatePatientCommand(UpdatePatientDto Dto) : ICommand<GetPatientDataDto>;

    public class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, GetPatientDataDto>
    {
        private readonly IPatientRepository _repository;

        public UpdatePatientCommandHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPatientDataDto> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdatePatientAsync(command.Dto);
            return result;
        }
    }
}
