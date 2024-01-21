using Application.Common.Interfaces.Repositories;
using Application.Patient.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Patient.Queries
{
    public record GetPatientDataQuery(Guid Id) : IRequest<GetPatientDataDto>;

    public class GetPatientDataHandler : IRequestHandler<GetPatientDataQuery, GetPatientDataDto>
    {
        private readonly IPatientRepository _repository;

        public GetPatientDataHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPatientDataDto> Handle(GetPatientDataQuery request, CancellationToken cancellationToken)
        {
            var patientData = await _repository.GetPatientDataAsync(request.Id);

            return patientData;
        }
    }

}
