using Bogus;
using Desk.Core.Handlers.Project.Commands;

namespace BA.Core.Functional.Tests.Handlers.Team.Fakers.Project;

public class Faker
{
    public CreateCommand FakeCreateCommand() => new Faker<CreateCommand>()
            .RuleFor(c => c.Id, f => 0)
            .RuleFor(c => c.Name, (f, c) => f.Lorem.Word())
            .RuleFor(c => c.Description, (f, c) => f.Lorem.Letter(1023))
            .Generate();

    public UpdateCommand FakeUpdatedommand(int id) => new Faker<UpdateCommand>()
            .RuleFor(c => c.Id, f => id)
            .RuleFor(c => c.Name, (f, c) => f.Lorem.Word())
            .RuleFor(c => c.Description, (f, c) => f.Lorem.Letter(1023))
            .Generate();
}