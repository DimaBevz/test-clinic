namespace Infrastructure.Dtos.Call.Common
{
    public class RecordingConfig
    {
        public int MaxSeconds { get; set; }
        public string? FileNamePrefix { get; set; }
        public VideoConfig? VideoConfig { get; set; }
        public AudioConfig? AudioConfig { get; set; }
        public StorageConfig? StorageConfig { get; set; }
        public DyteBucketConfig? DyteBucketConfig { get; set; }
        public LiveStreamingConfig? LiveStreamingConfig { get; set; }
    }
}
