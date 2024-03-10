using Application.BugReport.DTOs.RequestDTOs;
using Application.BugReport.DTOs.ResponseDTOs;
using Application.Common.DTOs;

namespace Application.Common.Interfaces.Repositories;

public interface IBugReportRepository
{
    public Task<SentBugReportDto?> SendBugReportAsync(
        SendBugReportDto sendBugReportDto, 
        FileDataDto?[] fileDataDtos,
        Guid userId,
        CancellationToken cancellationToken);

    public Task<List<GetBugReportsDto>> GetBugReportsAsync(CancellationToken cancellationToken);
}