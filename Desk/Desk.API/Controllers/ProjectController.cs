using Desk.Core.Endpoints;
using Desk.Core.Handlers.Project.Commands;
using Desk.Core.Handlers.Project.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desk.API.Controllers;

public class ProjectController : BaseEndpoint
{
    public ProjectController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateCommand command) => await Send<CreateCommand, ProjectModel>(command);

    [HttpPost("Get")]
    public async Task<ActionResult> Get([FromBody] GetCommand command) => await Send<GetCommand, ProjectModel>(command);
    [HttpPost("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateCommand command) => await Send<UpdateCommand, ProjectModel>(command);
    [HttpPost("Delete")]
    public async Task<ActionResult> Delete([FromBody] DeleteCommand command) => await Send<DeleteCommand, Unit>(command);
}