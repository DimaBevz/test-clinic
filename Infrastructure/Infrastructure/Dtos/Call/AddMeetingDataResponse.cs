using Infrastructure.Dtos.Call.Common;

namespace Infrastructure.Dtos.Call
{
    public class AddMeetingDataResponse
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? PreferredRegion { get; set; }
        public string? CreatedAt { get; set; }
        public bool RecordOnStart { get; set; }
        public string? UpdatedAt { get; set; }
        public bool LiveStreamOnStart { get; set; }
        public string? Status { get; set; }
        public RecordingConfig? RecordingConfig { get; set; }
    }
}
