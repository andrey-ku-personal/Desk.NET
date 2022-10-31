using Desk.Shared.Queries;
using System.Linq.Expressions;

namespace Desk.Identity.Handlers.User.Queries;

public class GetQuery : BaseQuery<Domain.Entities.User>
{
    public int? Id { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }

    public override Expression<Func<Domain.Entities.User, bool>> GetExpression()
    {
        return filter => Id != null ? filter.Id == Id : UserId != null ? filter.UserId == UserId : filter.Email == Email;
    }
}