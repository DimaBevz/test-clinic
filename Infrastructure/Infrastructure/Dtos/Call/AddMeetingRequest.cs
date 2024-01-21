using Infrastructure.Dtos.Call.Common;

namespace Infrastructure.Dtos.Call
{
    public class AddMeetingRequest
    {
        public string? Title { get; set; }
        public string? PreferredRegion { get; set; }
        public bool? RecordOnStart { get; set; }
        public bool? LiveStreamOnStart { get; set; }
        public RecordingConfig? RecordingConfig { get; set; }
    }
}
