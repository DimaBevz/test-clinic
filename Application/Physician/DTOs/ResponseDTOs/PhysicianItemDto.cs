using Application.Position.DTOs.ResponseDTOs;

namespace Application.Physician.DTOs.ResponseDTOs
{
    public record PhysicianItemDto
    (
        Guid Id,
        string FirstName,
        string LastName,
        string? Patronymic,
        string? PhotoUrl,
        float? Rating,
        string? Bio,
        bool IsApproved,
        int? Experience = null,
        int CommentsCount = 0,
        List<PositionDto>? Positions = null
    );
}
