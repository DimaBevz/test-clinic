namespace Application.Session.DTOs.Response
{
    public record UserInfoDto(Guid Id,
                              string LastName,
                              string FirstName,
                              string? Patronymic);

}
