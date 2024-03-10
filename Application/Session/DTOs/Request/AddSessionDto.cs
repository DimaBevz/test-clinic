namespace Application.Session.DTOs.Request
{
    public record AddSessionDto
    (
        DateOnly SessionDate,
        DateTime StartTime,
        DateTime EndTime,
        Guid PhysicianId,
        string Description,
        int CurrentPainScale,
        int AveragePainScaleLastMonth,
        int HighestPainScaleLastMonth
    );
}
