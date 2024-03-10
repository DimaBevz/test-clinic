using Application.BugReport.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Mediator;

namespace Application.BugReport.Queries;

public record GetBugReportsQuery() : IQuery<List<GetBugReportsDto>> ;

public class GetBugReportsQueryHandler : IQueryHandler<GetBugReportsQuery, List<GetBugReportsDto>>
{
    private readonly IBugReportRepository _bugReportRepository;
    
    public GetBugReportsQueryHandler(IBugReportRepository bugReportRepository)
    {
        _bugReportRepository = bugReportRepository;
    }
    
    public ValueTask<List<GetBugReportsDto>> Handle(GetBugReportsQuery query, CancellationToken cancellationToken)
    {
        return new ValueTask<List<GetBugReportsDto>>(_bugReportRepository.GetBugReportsAsync(cancellationToken));
    }
}