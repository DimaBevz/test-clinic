namespace Infrastructure.Dtos.Call.Common
{
    public class StorageConfig
    {
        public required string Type { get; set; }
        public string? AccessKey { get; set; }
        public string? Secret { get; set; }
        public string? Bucket { get; set; }
        public string? Region { get; set; }
        public string? Path { get; set; }
        public string? AuthMethod { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? PrivateKey { get; set; }
    }
}
