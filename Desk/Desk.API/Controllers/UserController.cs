using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Handlers.User.Models;
using Desk.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Desk.API.Controllers;

public class UserController : BaseEndpoint
{
    public UserController(IMediator mediator) : base(mediator)
    {

    }

    [HttpPost("User/Create")]
    [OpenApiOperation(
        operationId: "User.Create",
        summary: "Create user",
        description: "Create user")
    ]
    [OpenApiTags("User")]
    public async Task<ActionResult> Create([FromBody] CreateCommand command) => await Send<CreateCommand, UserModel>(command);
}