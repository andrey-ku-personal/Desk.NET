using BA.Core.Queries;
using System.Linq.Expressions;

namespace Desk.Core.Handlers.Project.Queries;

public class GetQuery : BaseQuery<Domain.Entities.Project>
{
    public int Id { get; set; }

    public override Expression<Func<Domain.Entities.Project, bool>> GetExpression()
    {
        return filter => filter.Id == Id;
    }
}