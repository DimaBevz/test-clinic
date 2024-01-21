using Application.Common.Interfaces.Repositories;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Physician.Queries
{
    public record GetPhysiciansByParamsQuery(GetPhysiciansByParamsDto Dto) : IQuery<GetPaginatedPhysiciansDto<PhysicianItemDto>>;

    public class GetPhysiciansByParamsQueryHandler : IQueryHandler<GetPhysiciansByParamsQuery, GetPaginatedPhysiciansDto<PhysicianItemDto>>
    {
        private readonly IPhysicianRepository _repository;

        public GetPhysiciansByParamsQueryHandler(IPhysicianRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPaginatedPhysiciansDto<PhysicianItemDto>> Handle(GetPhysiciansByParamsQuery request, CancellationToken cancellationToken)
        {
            var physicians = await _repository.GetPhysiciansAsync(request.Dto);
            return physicians;
        }
    }
}
