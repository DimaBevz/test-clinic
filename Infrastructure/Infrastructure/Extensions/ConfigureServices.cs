using System.Net.Http.Headers;
using System.Text;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Application.Common.Interfaces.Services;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var awsOptions = new AWSOptions
        {
            Credentials = new EnvironmentVariablesAWSCredentials()
        };

        services.AddDefaultAWSOptions(awsOptions);
        services.AddCognitoIdentity();
        services.AddAWSService<IAmazonS3>();

        services.AddScoped<ICallService, CallService>();
        services.AddScoped<IAuthService, CognitoAuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISessionService, SessionService>();
        
        services.Configure<DyteConfiguration>(configuration.GetSection(DyteConfiguration.Name));


        services.AddHttpClient<ICallService,CallService>((sp, client) =>
        {
            var config = sp.GetRequiredService<IOptions<DyteConfiguration>>();
            var authenticationString = $"{config.Value.OrganizationId}:{config.Value.ApiKey}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            client.BaseAddress = new Uri(config.Value.BaseUrl);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",base64EncodedAuthenticationString);
        });
            
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<QueueHostedService>();
        
        return services;
    }
}
