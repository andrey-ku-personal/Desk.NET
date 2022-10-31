using Desk.Core.Handlers.Project.Commands;
using Desk.Core.Handlers.Project.Models;
using Desk.Shared.Endpoints;
using Desk.Shared.Queries.Filter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Desk.API.Controllers;

public class ProjectController : BaseEndpoint
{
    public ProjectController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Project/Create")]
    [OpenApiOperation(
        operationId: "Project.Create",
        summary: "Create project",
        description: "Create project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> Create([FromBody] CreateCommand command) => await Send<CreateCommand, ProjectModel>(command);

    [HttpPost("Project/Get")]
    [OpenApiOperation(
        operationId: "Project.Get",
        summary: "Get project",
        description: "Get project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> Get([FromBody] GetCommand command) => await Send<GetCommand, ProjectModel>(command);

    [HttpPost("Project/Update")]
    [OpenApiOperation(
        operationId: "Project.Update",
        summary: "Update project",
        description: "Update project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> Update([FromBody] UpdateCommand command) => await Send<UpdateCommand, ProjectModel>(command);

    [HttpPost("Project/Delete")]
    [OpenApiOperation(
        operationId: "Project.Delete",
        summary: "Delete project",
        description: "Delete project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> Delete([FromBody] DeleteCommand command) => await Send<DeleteCommand, Unit>(command);

    [HttpPost("Project/GetAll")]
    [OpenApiOperation(
        operationId: "Project.GetAll",
        summary: "GetAll project",
        description: "GetAll project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> GetAll([FromBody] GetAllCommand command) => await Send<GetAllCommand, FilteredResult<ProjectModel>>(command);
}