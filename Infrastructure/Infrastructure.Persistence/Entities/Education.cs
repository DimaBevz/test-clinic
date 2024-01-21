namespace Infrastructure.Persistence.Entities
{
    public class Education
    {
        public string Institution { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
