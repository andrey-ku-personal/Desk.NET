using BA.Core.Functional.Tests.Handlers.Team.Fakers.Project;
using Desk.Shared.Exceptions;
using Desk.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Desk.Core.Tests.Handlers.Project;

[Collection(nameof(SliceFixture))]

public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_ProjcetModel()
    {
        var command = new Faker().FakeCreateCommand();
        var result = await _fixture.SendAsync(command);

        result.Id.ShouldBeGreaterThan(0);
        result.Name.ShouldBe(command.Name);
        result.Description.ShouldBe(command.Description);
    }

    [Theory]
    [InlineData(1)]
    public async Task Get_ValidationExcecption_On_Not_Zero_Id(int id)
    {
        var command = new Faker().FakeCreateCommand();
        command.Id = id;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_Name(string name)
    {
        var command = new Faker().FakeCreateCommand();
        command.Name = name;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Fact]
    public async Task Get_ValidationExcecption_On_Huge_Name()
    {
        var command = new Faker().FakeCreateCommand();
        command.Name = new Bogus.Faker().Random.String(257);

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Fact]
    public async Task Get_ValidationExcecption_On_Huge_Description()
    {
        var command = new Faker().FakeCreateCommand();
        command.Description = new Bogus.Faker().Random.String(1025);

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }
}
