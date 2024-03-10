namespace Application.TimetableTemplate.DTOs.Request
{
    public record UpdateTimetableTemplateDto
    (
        DateOnly StartDate, 
        DateOnly EndDate,
        Dictionary<DayOfWeek, DaySessionsDto> SessionTemplates, 
        Guid PhysicianDataId
    );
}
