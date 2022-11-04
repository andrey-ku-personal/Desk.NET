using MediatR;

namespace Desk.Identity.Handlers.Token.Commands;

public class AuthorizeCommand : IRequest<string>
{
    public string UserNameOrEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
}