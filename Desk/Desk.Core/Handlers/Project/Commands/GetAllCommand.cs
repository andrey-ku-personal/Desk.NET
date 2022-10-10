using Desk.Core.Handlers.Project.Models;
using Desk.Core.Queries.Filter;

namespace Desk.Core.Handlers.Project.Commands;

public record GetAllCommand : FilterModel, IRequest<FilteredResult<ProjectModel>>
{
}