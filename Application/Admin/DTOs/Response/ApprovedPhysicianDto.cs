namespace Application.Admin.DTOs.Response
{
    public record ApprovedPhysicianDto
    (
        Guid Id,
        bool IsApproved
    );
}
