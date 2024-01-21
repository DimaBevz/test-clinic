namespace Infrastructure.Persistence.Entities;

public class TestResultDetail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid TestResultId { get; set; }
    public TestResult TestResult { get; set; } = null!;
    
    public Guid TestQuestionId { get; set; }
    public TestQuestion TestQuestion { get; set; } = null!;
    
    public Guid TestOptionId { get; set; }
    public TestOption TestOption { get; set; } = null!;
}