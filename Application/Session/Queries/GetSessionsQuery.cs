using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Session.Queries
{
    public record GetSessionsQuery(GetSessionsRequestDto Filters) : IRequest<GetSessionsResponseDto>;

    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, GetSessionsResponseDto>
    {
        private readonly ISessionRepository _sessionRepository;
        private const string ExceptionMessage = "Patient and physician Id should not be null simultaniously";

        public GetSessionsQueryHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }
        public async ValueTask<GetSessionsResponseDto> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            var response = await _sessionRepository.GetSessionsAsync(request.Filters, cancellationToken);

            if (response is null)
            {
                throw new ApplicationException(ExceptionMessage);
            }

            return response;
        }
    }
}
