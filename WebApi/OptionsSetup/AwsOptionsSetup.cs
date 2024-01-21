using Application.Common.Options;
using Microsoft.Extensions.Options;

namespace WebApi.OptionsSetup;

public class AwsOptionsSetup: IConfigureOptions<AwsOptions>
{
    private const string SectionName = "AWS";
    private readonly IConfiguration _configuration;
    
    public AwsOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(AwsOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}