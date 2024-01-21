using Application.Common.Interfaces.Repositories;
using Application.Physician.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Physician.Queries
{
    public record GetPhysicianDataQuery(Guid Id) : IRequest<GetPhysicianDataDto>;

    public class GetPhysicianDataHandler : IRequestHandler<GetPhysicianDataQuery, GetPhysicianDataDto>
    {
        private readonly IPhysicianRepository _repository;

        public GetPhysicianDataHandler(IPhysicianRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPhysicianDataDto> Handle(GetPhysicianDataQuery request, CancellationToken cancellationToken)
        {
            var physicianData = await _repository.GetPhysicianDataAsync(request.Id);

            return physicianData;
        }
    }
}
