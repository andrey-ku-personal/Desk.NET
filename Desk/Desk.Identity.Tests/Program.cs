using Desk.Domain;
using Desk.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Desk.Identity;
using Desk.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainDependencies(builder.Configuration);
builder.Services.AddMigrationsDependencies(builder.Configuration);
builder.Services.AddSharedDependencies();
builder.Services.AddSharedSerializer();
builder.Services.AddSharedCors();
builder.Services.AddIdentityDependencies();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();