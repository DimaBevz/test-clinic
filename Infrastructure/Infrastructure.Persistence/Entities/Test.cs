using Application.Common.Enums;

namespace Infrastructure.Persistence.Entities;

public class Test
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TestType Type { get; set; }
}