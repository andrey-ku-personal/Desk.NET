using Desk.Identity.Handlers.User.Models;
using Desk.Identity.Options;
using Desk.Identity.Services;
using Desk.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;

namespace Desk.Identity;

public static class InjectionExtension
{
    public static void AddIdentityDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        services.Configure<JwtOptions>(jwtSection);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = jwtSection.Get<JwtOptions>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        context.Response
                            .WriteAsync(JsonConvert.SerializeObject(new Error("Authorization exception", "Token expired. Please, authorize.")))
                            .Wait();
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddTransient<IUserStore<UserModel>, UserStore>();
        services.AddTransient<IUserPasswordStore<UserModel>, UserStore>();
        services.AddTransient<IPasswordHasher<UserModel>, BCryptPasswordHasher>();
    }
}