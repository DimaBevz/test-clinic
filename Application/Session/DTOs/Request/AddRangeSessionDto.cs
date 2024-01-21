namespace Application.Session.DTOs.Request
{
    public record AddRangeSessionDto(DateOnly StartDate, 
                                    DateOnly EndDate,
                                    Dictionary<DayOfWeek, List<SessionTemplateDto>> SessionTemplates, 
                                    Guid PhysicianDataId);
}
