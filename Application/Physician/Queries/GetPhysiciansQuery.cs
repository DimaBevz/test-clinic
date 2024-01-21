using Application.Common.Interfaces.Repositories;
using Application.Physician.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Physician.Queries
{
    public record GetPhysiciansQuery() : IQuery<GetPhysiciansDto>;

    public class GetPhysiciansQueryHandler : IQueryHandler<GetPhysiciansQuery, GetPhysiciansDto>
    {
        private readonly IPhysicianRepository _repository;

        public GetPhysiciansQueryHandler(IPhysicianRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPhysiciansDto> Handle(GetPhysiciansQuery request, CancellationToken cancellationToken)
        {
            var physicians = await _repository.GetPhysiciansAsync();
            return physicians;
        }
    }
}
