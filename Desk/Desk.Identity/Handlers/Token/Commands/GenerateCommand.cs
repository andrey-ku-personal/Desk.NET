using MediatR;

namespace Desk.Identity.Handlers.Token.Commands;

public class GenerateCommand : IRequest<string>
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
}