using Desk.Identity.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Desk.Core;

public static class InjectionExtension
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        return services;
    }
}