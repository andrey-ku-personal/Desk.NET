using Desk.Core.Functional.Tests.Fixtures;
using Desk.Identity.Handlers.Token.Commands;
using Desk.Identity.Tests.Handlers.User.Fakers;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Desk.Identity.Tests.Handlers.Token;

[Collection(nameof(SliceFixture))]

public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_Token()
    {
        var createUserCommand = new Faker().FakeCreateCommand();
        var user = await _fixture.SendAsync(createUserCommand);

        var command = new AuthorizeCommand() { UserNameOrEmail = user.UserName, Password = createUserCommand.Password };
        var result = await _fixture.SendAsync(command);

        result.ShouldNotBeNull(result);
        result.Length.ShouldBeGreaterThan(0);
    }
}