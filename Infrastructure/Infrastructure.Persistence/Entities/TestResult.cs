namespace Infrastructure.Persistence.Entities;

public class TestResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int TotalScore { get; set; }
    
    public Guid TestCriteriaId { get; set; }
    public TestCriteria TestCriteria { get; set; } = null!;
    
    public Guid TestId { get; set; }
    public Test Test { get; set; } = null!;
    
    public Guid PatientDataId { get; set; }
    public PatientData PatientData { get; set; } = null!;
}