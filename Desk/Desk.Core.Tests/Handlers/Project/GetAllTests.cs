using BA.Core.Functional.Tests.Handlers.Team.Fakers.Project;
using Desk.Core.Functional.Tests.Fixtures;
using Desk.Core.Handlers.Project.Commands;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Desk.Core.Tests.Handlers.Project;

[Collection(nameof(SliceFixture))]
public class GetAllTests
{
    private readonly SliceFixture _fixture;

    public GetAllTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task GetAll_Projects()
    {
        var command1 = new Faker().FakeCreateCommand();
        var created1 = await _fixture.SendAsync(command1);

        var command2 = new Faker().FakeCreateCommand();
        var created2 = await _fixture.SendAsync(command2);

        var result = await _fixture.SendAsync(new GetAllCommand());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);

        result.Result.FirstOrDefault(x => x.Id == created1.Id).ShouldNotBeNull();
        result.Result.FirstOrDefault(x => x.Id == created2.Id).ShouldNotBeNull();
    }

    [Fact]
    public async Task GetAll_TeamModel_By_Search_By_Text()
    {
        var command = new Faker().FakeCreateCommand();
        var created = await _fixture.SendAsync(command);

        var aa = created.Description.Substring(0, 10);

        var result1 = await _fixture.SendAsync(new GetAllCommand { SearchByText = $"{created.Name}" });
        var result2 = await _fixture.SendAsync(new GetAllCommand { SearchByText = $"{created.Description[..10]}" });

        result1.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result1.Result.FirstOrDefault(x => x.Id == created.Id).ShouldNotBeNull();

        result2.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result2.Result.FirstOrDefault(x => x.Id == created.Id).ShouldNotBeNull();
    }
}