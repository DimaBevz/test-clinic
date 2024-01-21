using Application.Common.Interfaces.Repositories;
using Application.Position.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Position.Queries
{
    public record GetPositionsQuery() : IQuery<GetPositionsDto>;

    public class GetPositionsQueryHandler : IQueryHandler<GetPositionsQuery, GetPositionsDto>
    {
        private readonly IPositionRepository _positionRepository;

        public GetPositionsQueryHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async ValueTask<GetPositionsDto> Handle(GetPositionsQuery query, CancellationToken cancellationToken)
        {
            var result = await _positionRepository.GetPositionsAsync();
            return result;
        }
    }
}
