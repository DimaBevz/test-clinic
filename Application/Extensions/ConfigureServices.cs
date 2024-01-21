using Application.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(
            options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            }
        );

        return services;
    }
}