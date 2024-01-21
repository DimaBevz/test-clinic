namespace Infrastructure.Dtos.Call;

public class AddParticipantToMeetingRequest
{
    public required string Name { get; set; }
    public required string Picture { get; set; }
    public required string PresetName { get; set; }
    public required string CustomParticipantId { get; set; }
}
