namespace Desk.Core.Handlers.Project.Commands;

public class DeleteCommand : IRequest<Unit>
{
    public int Id { get; set; }
}