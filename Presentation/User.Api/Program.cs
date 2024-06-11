using User.Api.Validators;
using User.Application;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using User.Infrastructure.EFCore;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables("TAPPIT_")
    .AddCommandLine(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
        .Enrich.FromLogContext()
        .Enrich.WithSpan();

    if (context.HostingEnvironment.IsDevelopment())
    {
        configuration
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "[{TraceId} {Timestamp:HH:mm:ss} {Level:u3}] {Message} {NewLine}{Exception}");
    }
    else
    {
        configuration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.IdentityModel", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .WriteTo.Console(new ElasticsearchJsonFormatter());
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(container =>
    {
        container.RegisterModule(new EFCoreLayer(builder.Configuration));
        container.RegisterModule(new ApplicationLayer(builder.Configuration));

        container.RegisterType<HttpContextAccessor>()
            .As<IHttpContextAccessor>()
            .InstancePerLifetimeScope();
    });

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    options.GetLevel = (ctx, _, ex) =>
    {
        var requestPath = ctx.Request.Path;
                
        if (requestPath == "/metrics")
        {
            return LogEventLevel.Debug;
        }
                
        return LogEventLevel.Information;
    };
});

app.UseCors();
app.UseForwardedHeaders();
app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.Run();