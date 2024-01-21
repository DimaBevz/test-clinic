namespace Infrastructure.Dtos.Call;

public class AddParticipantToMeetingResponse
{
    public bool Success { get; set; }
    public AddParticipantToMeetingDataResponse? Data { get; set; }
}
