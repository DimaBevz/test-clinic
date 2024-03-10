using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Extensions;
using WebApi.Extensions;
using WebApi.Hubs;

const string corsPolicy = "corsPolicy";

var builder = WebApplication.CreateBuilder(args);
builder
    .Configuration
    .AddEnvironmentVariables("AWS");
builder.Services
    .RegisterInfrastructureServices(builder.Configuration)
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterApplicationServices()
    .RegisterWebApiServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, corsBuilder => {
        corsBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:4000");
    });
});
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddSpaStaticFiles(spa =>
{
    spa.RootPath = "wwwroot";
}); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();
app.UseSpaStaticFiles();
app.UseSpa(spa => spa.Options.SourcePath = "ClientApp");

app.MapHub<ChatHub>("/hub/chat-hub");
app.MapHub<CallHub>("/hub/call-hub");

app.MapControllers();

app.Run();
