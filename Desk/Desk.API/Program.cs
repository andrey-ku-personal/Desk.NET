using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Desk.Core;
using Desk.Core.Exceptions.Extensions;
using Desk.Domain;
using Desk.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
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

builder.Services.AddAutoMapper((serviceProvider, autoMapper) =>
{
    autoMapper.AddCollectionMappers();
    autoMapper.UseEntityFrameworkCoreModel<EntitiesDbContext>(serviceProvider);
}, Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy
    (
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
    );
});

builder.Services.AddCoreDependencies();
builder.Services.AddDomainDependencies(builder.Configuration);
builder.Services.AddMigrationsDependencies(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();