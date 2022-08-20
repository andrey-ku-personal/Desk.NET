using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Desk.Migrations;

public static class InjectionExtension
{
    public static IServiceCollection AddMigrationsDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(config => config
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .Configure<RunnerOptions>(opt => {
                    opt.Tags = new[] { "Desk" };
                })
                .BuildServiceProvider(false);

        using var provider = services.BuildServiceProvider();
        using (var scope = provider.CreateScope())
        {
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator!.MigrateUp();
        }

        return services;
    }
}