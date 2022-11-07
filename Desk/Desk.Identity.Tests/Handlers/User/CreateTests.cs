using Desk.Core.Functional.Tests.Fixtures;
using Desk.Identity.Tests.Handlers.User.Fakers;
using Desk.Shared.Exceptions;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Desk.Identity.Tests.Handlers.User;

[Collection(nameof(SliceFixture))]

public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_UserModel()
    {
        var command = new Faker().FakeCreateCommand();
        var result = await _fixture.SendAsync(command);

        result.Id.ShouldBeGreaterThan(0);
        result.UserName.ShouldBe(result.UserName);
        result.FirstName.ShouldBe(result.FirstName);
        result.LastName.ShouldBe(result.LastName);
        result.CreateTime.ShouldBeGreaterThan(default);
        result.LastLoginTime.ShouldBeGreaterThan(default);
        result.Website.ShouldBe(result.Website);
        result.Description.ShouldBe(result.Description);
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
    public async Task Get_ValidationExcecption_On_Empty_FirstName(string firstName)
    {
        var command = new Faker().FakeCreateCommand();
        command.FirstName = firstName;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Empty_LastName(string lastName)
    {
        var command = new Faker().FakeCreateCommand();
        command.FirstName = lastName;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData(null)]
    [InlineData("123456789")]
    [InlineData("123456789012")]
    [InlineData("12345678901a")]
    [InlineData("12345678901A")]
    [InlineData("12345678901@")]
    public async Task Get_ValidationExcecption_On_Invalid_Password(string password)
    {
        var command = new Faker().FakeCreateCommand();
        command.Password = password;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("12345678901a")]
    public async Task Get_ValidationExcecption_On_Invalid_Email(string email)
    {
        var command = new Faker().FakeCreateCommand();
        command.Email = email;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }
}