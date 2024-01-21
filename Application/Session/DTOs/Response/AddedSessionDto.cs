namespace Application.Session.DTOs.Response
{
    public record AddedSessionDto(Guid Id,
                                  DateTime StartTime,
                                  DateTime EndTime,
                                  Guid PhysicianId,
                                  string Complaints,
                                  int CurrentPainScale,
                                  int AveragePainScaleLastMonth,
                                  int HighestPainScaleLastMonth);
}
