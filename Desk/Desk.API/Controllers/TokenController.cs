using Desk.Identity.Handlers.Token.Commands;
using Desk.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Desk.API.Controllers;

public class TokenController : BaseEndpoint
{
    public TokenController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Token/Generate")]
    [OpenApiOperation(
        operationId: "Token.Generate",
        summary: "Generate Token",
        description: "Generate Token")
    ]
    [OpenApiTags("Token")]
    public async Task<ActionResult> Create([FromBody] AuthorizeCommand command) => await Send<AuthorizeCommand, string>(command);
}