namespace Infrastructure.Configurations;

public class DyteConfiguration
{
    public const string Name = "Dyte";
    public required string BaseUrl { get; set; }
    public required string ApiKey { get; set; }
    public required string OrganizationId { get; set; }
};
