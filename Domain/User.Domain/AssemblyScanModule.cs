using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace User.Domain;

public abstract class AssemblyScanModule(IConfiguration configuration) : Autofac.Module
{
    protected abstract Assembly Assembly { get; }

    protected override void Load(ContainerBuilder builder) =>
        builder.RegisterAssemblyTypes(Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
}