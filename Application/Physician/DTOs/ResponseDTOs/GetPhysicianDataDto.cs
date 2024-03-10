using Application.Position.DTOs.ResponseDTOs;

namespace Application.Physician.DTOs.ResponseDTOs
{
    public record GetPhysicianDataDto
    (
        float? Rating,
        string? Bio,
        bool IsApproved,
        int? Experience = null,
        List<PositionDto>? Positions = null
    );
}
