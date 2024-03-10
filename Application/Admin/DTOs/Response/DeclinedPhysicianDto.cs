namespace Application.Admin.DTOs.Response
{
    public record DeclinedPhysicianDto
    (
        Guid Id,
        bool IsApproved,
        string Message
    );
}
