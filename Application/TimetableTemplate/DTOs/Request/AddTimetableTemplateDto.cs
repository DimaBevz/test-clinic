namespace Application.TimetableTemplate.DTOs.Request
{
    public record AddTimetableTemplateDto(DateOnly StartDate, 
                                          DateOnly EndDate,
                                          Dictionary<DayOfWeek, DaySessionsDto> SessionTemplates, 
                                          Guid PhysicianDataId);
}
