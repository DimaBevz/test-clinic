using Application.Common.Enums;

namespace Infrastructure.Persistence.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly? Birthday { get; set; }
        public string? Sex { get; set; } = string.Empty;
        public RoleType Role { get; set; }
        public bool IsBanned { get; set; }

        public UserPhotoData? UserPhotoData { get; set; }
        public List<Document>? Documents { get; set; }
        public PatientData? PatientData { get; set; }
        public PhysicianData? PhysicianData { get; set; }
    }
}
