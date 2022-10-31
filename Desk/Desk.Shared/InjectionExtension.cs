using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Desk.Domain;
using Desk.Identity.Behaviours;
using Desk.Shared.Exceptions.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Desk.Shared;

public static class InjectionExtension
{
    public static void AddSharedDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper((serviceProvider, autoMapper) =>
        {
            autoMapper.AddCollectionMappers();
            autoMapper.UseEntityFrameworkCoreModel<EntitiesDbContext>(serviceProvider);
        }, GetAutoMapperProfilesFromAllAssemblies());

        services.AddValidatorsFromAssemblies(GetSolutionAssemblies());
        services.AddMediatR(GetSolutionAssemblies());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddSharedSerializer(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlerAttribute>();
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        });
    }

    public static void AddSharedCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy
            (
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
            );
        });
    }

    public static Assembly[] GetSolutionAssemblies()
        => AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName?.Contains("Desk") ?? false).ToArray();

    private static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies()
    {
        return from assembly in AppDomain.CurrentDomain.GetAssemblies()
               from aType in assembly.GetTypes()
               where aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile))
               select aType;
    }
}