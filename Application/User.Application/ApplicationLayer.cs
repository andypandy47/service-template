using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using User.Domain;
using User.Domain.Configuration;

namespace User.Application;

public class ApplicationLayer(IConfiguration configuration) : AssemblyScanModule(configuration)
{
    protected override Assembly Assembly => Assembly.GetExecutingAssembly();
    protected override void Load(ContainerBuilder builder)
    {
        var rabbitMqConfiguration = configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>() ?? throw new InvalidOperationException("RabbitMq config is null");
        builder.Register(x => Options.Create(rabbitMqConfiguration))
            .As<IOptions<RabbitMqConfig>>()
            .SingleInstance();
        
        var services = new ServiceCollection();
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConfiguration.Host, configurator =>
                {
                    configurator.Username(rabbitMqConfiguration.Username);
                    configurator.Password(rabbitMqConfiguration.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        builder.Populate(services);
        
        builder.RegisterAssemblyTypes(Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}