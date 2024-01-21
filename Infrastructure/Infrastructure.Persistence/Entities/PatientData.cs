using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Entities
{
    public class PatientData
    {
        [Key]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public string Settlement {  get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public int? Apartment { get; set; } 
        public string Institution { get; set; } = string.Empty;
        public string Position {  get; set; } = string.Empty;
    }
}
