using Application.Common.Interfaces.Repositories;
using Application.TimetableTemplate.DTOs.Response;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.TimetableTemplate.Queries
{
    public record GetAvailableTimetableQuery(Guid PhysicianId) : IQuery<GetAvailableTimetableDto>;

    public class GetAvailableTimetableQueryHandler : IQueryHandler<GetAvailableTimetableQuery, GetAvailableTimetableDto>
    {
        private readonly ITimetableRepository _timetableRepository;
        private const string ExceptionMessage = "There is no timetable for current physician";

        public GetAvailableTimetableQueryHandler(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async ValueTask<GetAvailableTimetableDto> Handle(GetAvailableTimetableQuery query, CancellationToken cancellationToken)
        {
            var result = await _timetableRepository.GetAvailableTimetableAsync(query.PhysicianId, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException(ExceptionMessage);
            }

            return result;
        }
    }
}
