using Desk.Identity.Handlers.User.Models;
using MediatR;

namespace Desk.Identity.Handlers.User.Commands;

public class CreateCommand : UserModel, IRequest<UserModel>
{
    public string Password { get; set; } = default!;
}