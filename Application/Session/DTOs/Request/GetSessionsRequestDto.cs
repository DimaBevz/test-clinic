using Application.Common.Enums;

namespace Application.Session.DTOs.Request
{
    public record GetSessionsRequestDto
    (
        Guid? PhysicianId, 
        Guid? PatientId, 
        DateTime? StartTime, 
        DateTime? EndTime,
        SessionSortType SortType
    );
}
