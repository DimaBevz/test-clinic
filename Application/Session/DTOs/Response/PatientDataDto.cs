namespace Application.Session.DTOs.Response
{
    public record PatientDataDto(Guid Id,
                                 string LastName,
                                 string FirstName,
                                 string? Patronymic,
                                 string? PhotoUrl);
}
