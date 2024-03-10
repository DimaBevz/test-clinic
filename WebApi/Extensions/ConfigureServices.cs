using WebApi.Filters;
using Application.Common.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.OptionsSetup;
using Swashbuckle.AspNetCore.Filters;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace WebApi.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection RegisterWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(opt => opt.Filters.Add(typeof(HttpExceptionFilter)));
        services.ConfigureOptions<AwsOptionsSetup>();
        services.Configure<S3BucketOptions>(configuration.GetSection("AWS:Bucket"));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Authentication Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JsonWebToken",
                Scheme = "Bearer"
            });
            options.AddEnumsWithValuesFixFilters();
        });

        services.AddSignalR();

        var awsOptions = services.BuildServiceProvider().GetService<IOptions<AwsOptions>>()!.Value;
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ctx =>
                    {
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = ctx =>
                    {
                        var tokenInQuery = ctx.Request.Query.TryGetValue("access_token", out var queryToken);
                        var tokenInHeader = ctx.Request.Headers.TryGetValue("authorization", out var headerToken);

                        if (tokenInQuery || tokenInHeader)
                        {
                            var path = ctx.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/hub", StringComparison.OrdinalIgnoreCase))
                            {
                                if(!string.IsNullOrEmpty(queryToken))
                                {
                                    ctx.Token = queryToken;                               
                                }
                                else if(!string.IsNullOrEmpty(headerToken))
                                {
                                    ctx.Token = headerToken.ToString().Replace("Bearer ", "");
                                }
                            }          
          
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        return Task.CompletedTask;
                    }
                };
                options.Authority = $"https://cognito-idp.{awsOptions.Region}.amazonaws.com/{awsOptions.UserPoolId}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://cognito-idp.{awsOptions.Region}.amazonaws.com/{awsOptions.UserPoolId}",
                    ValidateLifetime = true,
                    LifetimeValidator = (_, expires, _, _) => expires > DateTime.UtcNow,
                    ValidateAudience = false,
                    RoleClaimType = "custom:role"
                };
            });

        services.AddAuthorization();

        return services;
    }
}
