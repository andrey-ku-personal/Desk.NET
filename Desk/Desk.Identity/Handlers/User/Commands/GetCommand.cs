using Desk.Identity.Handlers.User.Models;
using MediatR;

namespace Desk.Identity.Handlers.User.Commands;

public class GetCommand : IRequest<UserModel>
{
    public int? Id { get; set; }
    public string? UserNameOrEmail { get; set; }
}
