using Desk.Core.Handlers.Project.Models;

namespace Desk.Core.Handlers.Project.Commands;

public class GetCommand : IRequest<ProjectModel>
{
    public int Id { get; set; }
}