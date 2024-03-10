using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(opts => opts.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPhysicianRepository, PhysicianRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<ITimetableRepository, TimetableRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IMilitaryDataRepository, MilitaryDataRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IMeetingHistoryRepository, MeetingHistoryRepository>();
        services.AddScoped<IBugReportRepository, BugReportRepository>();

        return services;
    }
}