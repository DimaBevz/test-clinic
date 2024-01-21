namespace Application.Call.DTOs;

public class CreateCallDto
{
    public required string Name { get; set; }
    public required string MeetingId { get; set; }
    public required string? Picture { get; set; }
    public required bool IsHost { get; set; }
    public required string UserId { get; set; }
}
