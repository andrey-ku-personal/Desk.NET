using Desk.Core.Handlers.Project.Models;

namespace Desk.Core.Handlers.Project.Commands;

public class CreateCommand : IRequest<ProjectModel>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
