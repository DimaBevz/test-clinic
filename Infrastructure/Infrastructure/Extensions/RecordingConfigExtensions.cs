using Infrastructure.Dtos.Call.Common;

namespace Infrastructure.Extensions
{
    internal static class RecordingConfigExtensions
    {
        public static RecordingConfig Default(this RecordingConfig config)
        {
            return new RecordingConfig
            {
                FileNamePrefix = "string",
                MaxSeconds = 60,
                VideoConfig = new VideoConfig
                {
                    Codec = "H264",
                    Width = 1280,
                    Height = 720,
                    Watermark = new Watermark
                    {
                        Position = "right bottom",
                        Size = new Size
                        {
                            Height = 5,
                            Width = 5
                        },
                        Url = "https://i.imgur.com/test.jpg"
                    }
                },
                AudioConfig = new AudioConfig
                {
                    Codec = "MP3",
                    Channel = "stereo"
                },
                StorageConfig = new StorageConfig
                {
                    Type = "aws",
                    AccessKey = "string",
                    AuthMethod = "KEY",
                    Path = "string",
                    Username = "string",
                    Bucket = "string",
                    Host = "string",
                    Password = "string",
                    Port = 0,
                    PrivateKey = "string",
                    Region = "eu-central-1",
                    Secret = "string"
                },
                DyteBucketConfig = new DyteBucketConfig
                {
                    Enabled = false
                },
                LiveStreamingConfig = new LiveStreamingConfig
                {
                    RtmpUrl = "rtmp://a.rtmp.youtube.com/live2"
                }
            };
        }
    }
}
