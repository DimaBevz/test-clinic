namespace Infrastructure.Persistence.Entities;

public class TestOption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public int Points { get; set; }
    public Guid TestQuestionId { get; set; }
    public TestQuestion TestQuestion { get; set; } = null!;
}