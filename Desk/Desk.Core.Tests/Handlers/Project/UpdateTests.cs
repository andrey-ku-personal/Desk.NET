using BA.Core.Functional.Tests.Handlers.Team.Fakers.Project;
using Desk.Shared.Exceptions;
using Desk.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Desk.Core.Tests.Handlers.Project;

[Collection(nameof(SliceFixture))]

public class UpdateTests
{
    private readonly SliceFixture _fixture;

    public UpdateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Update_ProjcetModel()
    {
        var createdCommand = new Faker().FakeCreateCommand();
        var created = await _fixture.SendAsync(createdCommand);

        var command = new Faker().FakeUpdatedommand(created.Id);
        var result = await _fixture.SendAsync(command);

        result.Id.ShouldBe(command.Id);
        result.Name.ShouldBe(command.Name);
        result.Description.ShouldBe(command.Description);
    }

    [Theory]
    [InlineData(0)]
    public async Task Get_ValidationExcecption_On_Not_Zero_Id(int id)
    {
        var createdCommand = new Faker().FakeCreateCommand();
        var created = await _fixture.SendAsync(createdCommand);

        var command = new Faker().FakeUpdatedommand(id);
        command.Id = id;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_Name(string name)
    {
        var createdCommand = new Faker().FakeCreateCommand();
        var created = await _fixture.SendAsync(createdCommand);

        var command = new Faker().FakeUpdatedommand(created.Id);
        command.Name = name;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Fact]
    public async Task Get_ValidationExcecption_On_Huge_Name()
    {
        var createdCommand = new Faker().FakeCreateCommand();
        var created = await _fixture.SendAsync(createdCommand);

        var command = new Faker().FakeUpdatedommand(created.Id);
        command.Name = new Bogus.Faker().Random.String(257);

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Fact]
    public async Task Get_ValidationExcecption_On_Huge_Description()
    {
        var createdCommand = new Faker().FakeCreateCommand();
        var created = await _fixture.SendAsync(createdCommand);

        var command = new Faker().FakeUpdatedommand(created.Id);
        command.Description = new Bogus.Faker().Random.String(1025);

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }
}