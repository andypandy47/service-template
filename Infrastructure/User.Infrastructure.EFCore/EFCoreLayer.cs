using System.Reflection;
using Autofac;
using User.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using User.Domain.Core.Configuration;

namespace User.Infrastructure.EFCore;

public class EFCoreLayer(IConfiguration configuration) : AssemblyScanModule(configuration)
{
    protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly)
            .Except<ApplicationDbContext>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        var databaseConfig = configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>() ?? throw new InvalidOperationException("Database config is null");
        builder.Register(x => Options.Create(databaseConfig))
            .As<IOptions<DatabaseConfig>>()
            .SingleInstance();

        var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbOptionsBuilder.UseNpgsql(databaseConfig.ConnectionString);

        builder.RegisterType<ApplicationDbContext>()
            .WithParameter("options", dbOptionsBuilder.Options)
            .InstancePerLifetimeScope();
    }
}