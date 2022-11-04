using Desk.Core.Functional.Tests.Fixtures;
using Desk.Identity.Handlers.Token.Commands;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Desk.Identity.Tests.Handlers.User.Fakers;

namespace Desk.Identity.Tests.Handlers.Token;

[Collection(nameof(SliceFixture))]
public class GenerateTests
{
    private readonly SliceFixture _fixture;

    public GenerateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Generate_Token()
    {
        var createUserCommand = new Faker().FakeCreateCommand();
        var user = await _fixture.SendAsync(createUserCommand);

        var command = new GenerateCommand() { Id = user.Id, UserName = user.UserName };
        var result = await _fixture.SendAsync(command);

        result.Length.ShouldBeGreaterThan(0);
    }
}