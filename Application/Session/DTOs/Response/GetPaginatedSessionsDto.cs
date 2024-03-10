namespace Application.Session.DTOs.Response
{
    public record GetPaginatedSessionsDto
    (
        List<SessionItemDto> Sessions,
        long TotalCount
    );
}
