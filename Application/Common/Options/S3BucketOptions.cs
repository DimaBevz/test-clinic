namespace Application.Common.Options
{
    public class S3BucketOptions
    {
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, string> Folders { get; set; } = null!;
    }
}
