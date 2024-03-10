namespace Infrastructure.Persistence.Entities;

public class TestQuestion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public Guid TestId { get; set; }
    public Test Test { get; set; } = null!;
    public List<TestOption>? TestOptions { get; set; }
}