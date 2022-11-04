using Bogus;
using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Handlers.User.Models;
using Desk.Identity.Tests.Extensions;

namespace Desk.Identity.Tests.Handlers.User.Fakers;

public class Faker
{
    public CreateCommand FakeCreateCommand() => new Faker<CreateCommand>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.UserName, (f, c) => f.Random.Word())
        .RuleFor(c => c.FirstName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.LastName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
        .RuleFor(c => c.Password, (f, c) => f.Internet.GeneratePassword(12, 16))
        .Generate();

    public UserModel FakeUserModel() => new Faker<UserModel>()
        .RuleFor(c => c.Id, f => 0)
        .RuleFor(c => c.UserName, (f, c) => f.Random.Word())
        .RuleFor(c => c.FirstName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.LastName, (f, c) => f.Lorem.Word())
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
        .RuleFor(c => c.Password, (f, c) => f.Internet.GeneratePassword(12, 16))
        .Generate();
}