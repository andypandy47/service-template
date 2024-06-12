using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using User.Domain;
using User.Domain.Configuration;

namespace User.Infrastructure.Redis;

public class RedisLayer(IConfiguration configuration) : AssemblyScanModule(configuration)
{
    protected override Assembly Assembly { get; }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        var redisConfiguration = configuration.GetSection("RedisConfig").Get<RedisConfig>() ?? throw new InvalidOperationException("Redis config is null");
        builder.Register(x => Options.Create(redisConfiguration))
            .As<IOptions<RedisConfig>>()
            .SingleInstance();
        
        //Register redis in here
    }
}