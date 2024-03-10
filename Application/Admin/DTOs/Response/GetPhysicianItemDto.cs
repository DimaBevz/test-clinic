namespace Application.Admin.DTOs.Response
{
    public record GetPhysicianItemDto
    (
        Guid Id,
        string LastName,
        string FirstName,
        string? Patronymic,
        string? PhotoUrl,
        int CountOfDocuments,
        string? AdminMessage
    );
}
