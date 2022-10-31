using Desk.Identity.Handlers.User.Models;
using Desk.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Desk.Identity;

public static class InjectionExtension
{
    public static void AddIdentityDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserStore<UserModel>, UserStore>();
    }
}