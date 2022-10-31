using Desk.Core.Handlers.Project.Models;
using Desk.Shared.Queries.Filter;

namespace Desk.Core.Handlers.Project.Commands;

public record GetAllCommand : FilterModel, IRequest<FilteredResult<ProjectModel>>
{
}