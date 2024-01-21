namespace Infrastructure.Persistence.Entities;

public class TestCriteria
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Min { get; set; }
    public int Max { get; set; }
    public string Verdict { get; set; } = string.Empty;
    
    public Guid TestId { get; set; }
    public Test Test { get; set; } = null!;
}