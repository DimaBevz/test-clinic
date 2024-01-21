namespace Application.Session.DTOs.Request
{
    public record GetSessionsRequestDto(Guid? PhysicianId, 
                                        Guid? PatientId, 
                                        DateTime? StartTime, 
                                        DateTime? EndTime,
                                        bool? ShowArchived,
                                        bool? ShowDeleted);
}
