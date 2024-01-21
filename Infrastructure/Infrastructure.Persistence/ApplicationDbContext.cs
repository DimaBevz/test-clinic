using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<PatientData> PatientData { get; set; } = null!;
        public DbSet<PhysicianData> PhysicianData { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<SessionDetail> SessionDetails { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<UserPhotoData> UserPhotoData { get; set; } = null!;
        public DbSet<TimetableTemplate> TimetableTemplates { get; set; } = null!;
        public DbSet<SessionDayTemplate> SessionDayTemplates { get; set; } = null!;
        public DbSet<SessionTemplate> SessionTemplates { get; set; } = null!;
        public DbSet<ChatHistory> ChatHistories { get; set; } = null!;
        public DbSet<PhysicianSpecialty> PhysicianSpecialties { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<TestQuestion> TestsQuestions { get; set; } = null!;
        public DbSet<TestOption> TestOptions { get; set; } = null!;
        public DbSet<TestResult> TestResults { get; set; } = null!;
        public DbSet<TestResultDetail> TestResultDetails { get; set; } = null!;
        public DbSet<TestCriteria> TestCriteria { get; set; } = null!;
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
