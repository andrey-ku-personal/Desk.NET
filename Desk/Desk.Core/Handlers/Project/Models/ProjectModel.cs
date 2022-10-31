namespace Desk.Core.Handlers.Project.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Website { get; set; } = default!;
    public string Description { get; set; } = default!;
}