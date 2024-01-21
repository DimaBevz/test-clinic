namespace Infrastructure.Dtos.Call.Common
{
    public class VideoConfig
    {
        public string? Codec { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Watermark? Watermark { get; set; }
    }
}
