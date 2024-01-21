namespace Application.User.DTOs.ResponseDTOs;

public record TokenDto(string IdToken, string RefreshToken, DateTime ExpiresAt);

