using System.Reflection;
using Microsoft.EntityFrameworkCore;
using User.Domain.Core.Entities;

namespace User.Infrastructure.EFCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public const string Schema = "User";
    public const string MigrationHistoryTableName = "__EFMigrationsHistory";
    
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}