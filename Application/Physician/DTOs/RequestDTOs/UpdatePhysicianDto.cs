namespace Application.Physician.DTOs.RequestDTOs
{
    public record UpdatePhysicianDto
    (
        Guid Id,
        DateOnly? Experience = null,
        string? Bio = null,
        List<Guid>? PositionIds = null
    );
}
