using Desk.Core.Functional.Tests.Fixtures;
using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Tests.Handlers.User.Fakers;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Desk.Identity.Tests.Handlers.User;

[Collection(nameof(SliceFixture))]

public class GetTests
{
    private readonly SliceFixture _fixture;

    public GetTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Get_User_By_Id()
    {
        var createCommand = new Faker().FakeCreateCommand();
        var user = await _fixture.SendAsync(createCommand);

        var command = new GetCommand() { Id = user.Id };
        var result = await _fixture.SendAsync(command);

        result!.ShouldNotBeNull();
        result!.Id.ShouldBe((int)command!.Id);
    }

    [Fact]
    public async Task Get_User_By_UserId()
    {
        var createCommand = new Faker().FakeCreateCommand();
        var user = await _fixture.SendAsync(createCommand);

        var command = new GetCommand() { UserNameOrEmail = user.UserName };
        var result = await _fixture.SendAsync(command);

        result.ShouldNotBeNull();
        result.UserName.ShouldBe(command.UserNameOrEmail);
    }

    [Fact]
    public async Task Get_User_By_Email()
    {
        var createCommand = new Faker().FakeCreateCommand();
        var user = await _fixture.SendAsync(createCommand);

        var command = new GetCommand() { UserNameOrEmail = user.Email };
        var result = await _fixture.SendAsync(command);

        result.ShouldNotBeNull();
        result.Email.ShouldBe(command.UserNameOrEmail);
    }
}