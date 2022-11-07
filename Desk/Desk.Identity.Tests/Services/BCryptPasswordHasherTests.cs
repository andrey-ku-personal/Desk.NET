using Desk.Core.Functional.Tests.Fixtures;
using Desk.Identity.Services;
using Desk.Identity.Tests.Handlers.User.Fakers;
using Shouldly;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace Desk.Identity.Tests.Services;

[Collection(nameof(SliceFixture))]
public class BCryptPasswordHasherTests
{
    private readonly SliceFixture _fixture;

    public BCryptPasswordHasherTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public void Hash_And_Verify_Password()
    {
        var model = new Faker().FakeUserModel();

        var passwordHasher = new BCryptPasswordHasher();

        var hash = passwordHasher.HashPassword(model, model.Password);
        hash.ShouldNotBeNullOrWhiteSpace();
        hash.Length.ShouldBeGreaterThan(0);

        var verified = passwordHasher.VerifyHashedPassword(model, hash, model.Password);
        verified.ShouldBe(PasswordVerificationResult.Success);
    }

    [Fact]
    public void Wrong_Password_To_Verify_Password()
    {
        var model = new Faker().FakeUserModel();

        var passwordHasher = new BCryptPasswordHasher();

        var hash = passwordHasher.HashPassword(model, model.Password);

        var verified = passwordHasher.VerifyHashedPassword(model, hash, model.Password + "123");
        verified.ShouldBe(PasswordVerificationResult.Failed);
    }
}