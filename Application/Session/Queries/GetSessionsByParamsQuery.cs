using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Session.Queries
{
    public record GetSessionsByParamsQuery(GetSessionsByParamsDto Params) : IQuery<GetPaginatedSessionsDto>;

    public class GetSessionsByParamsQueryHandler : IQueryHandler<GetSessionsByParamsQuery, GetPaginatedSessionsDto>
    {
        private readonly ISessionRepository _sessionRepository;
        private const string IdExceptionMessage = "Params did not specify Id of user";

        public GetSessionsByParamsQueryHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async ValueTask<GetPaginatedSessionsDto> Handle(GetSessionsByParamsQuery query, CancellationToken cancellationToken)
        {
            var result = await _sessionRepository.GetSessionsByParamsAsync(query.Params, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException(IdExceptionMessage);
            }

            return result;
        }
    }

}
