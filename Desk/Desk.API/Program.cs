using Desk.Core;
using Desk.Domain;
using Desk.Migrations;
using Desk.Identity;
using Desk.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainDependencies(builder.Configuration);
builder.Services.AddMigrationsDependencies(builder.Configuration);
builder.Services.AddSharedDependencies();
builder.Services.AddSharedSerializer();
builder.Services.AddSharedCors();
builder.Services.AddCoreDependencies();
builder.Services.AddIdentityDependencies();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();