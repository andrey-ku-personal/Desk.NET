using Desk.Shared.Queries;
using System.Linq.Expressions;

namespace Desk.Identity.Handlers.User.Queries;

public class GetQuery : BaseQuery<Domain.Entities.User>
{
    public int? Id { get; set; }
    public string? UserNameOrEmail { get; set; }

    public override Expression<Func<Domain.Entities.User, bool>> GetExpression()
    {
        return filter => Id != null ? filter.Id == Id : filter.UserName == UserNameOrEmail || filter.Email == UserNameOrEmail;
    }
}