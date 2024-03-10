using Application.Common.Interfaces.Repositories;
using Application.TimetableTemplate.DTOs.Request;
using Application.TimetableTemplate.DTOs.Response;
using Mediator;

namespace Application.TimetableTemplate.Queries
{
    public record GetTimetableTemplateQuery(GetTimetableTemplateDto Request) : IQuery<GetTimetablesDto?>;

    public class GetTimetableTemplateQueryHandler : IQueryHandler<GetTimetableTemplateQuery, GetTimetablesDto?>
    {
        private readonly ITimetableRepository _timetableRepository;

        public GetTimetableTemplateQueryHandler(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async ValueTask<GetTimetablesDto?> Handle(GetTimetableTemplateQuery query, CancellationToken cancellationToken)
        {
            var result = await _timetableRepository.GetTimetablesAsync(query.Request, cancellationToken);

            return result;
        }
    }
}
