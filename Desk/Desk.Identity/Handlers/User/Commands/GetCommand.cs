using Desk.Identity.Handlers.User.Models;
using MediatR;

namespace Desk.Identity.Handlers.User.Commands;

public class GetCommand : IRequest<UserModel>
{
    public int? Id { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
}
