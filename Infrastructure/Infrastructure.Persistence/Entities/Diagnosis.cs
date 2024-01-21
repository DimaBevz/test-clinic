
namespace Infrastructure.Persistence.Entities
{
    public class Diagnosis
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
    }
}
