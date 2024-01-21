namespace Application.User.DTOs.RequestDTOs
{
    public class UpdateUserPhotoDto
    {
        public string PhotoObjectKey { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
