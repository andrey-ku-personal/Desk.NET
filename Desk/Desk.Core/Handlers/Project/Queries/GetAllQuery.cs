using Desk.Core.Queries;
using System.Linq.Expressions;

namespace Desk.Core.Handlers.Project.Queries;

public class GetAllQuery : BaseSortQuery<Domain.Entities.Project>
{
    public string? SearchByText { get; set; }

    public override Expression<Func<Domain.Entities.Project, bool>> GetExpression()
    {
        var filter = base.GetExpression();

        if (!string.IsNullOrWhiteSpace(SearchByText))
        {
            foreach (var word in SearchByText.Split())
            {
                filter = filter.And(x => x.Name.Contains(word) || x.Description.Contains(word));
            }
        }

        return filter;
    }

    public override List<Expression<Func<Domain.Entities.Project, object>>> GetIncludes()
    {
        var includes = base.GetIncludes();
        return includes;
    }
}