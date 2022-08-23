using System.Linq.Expressions;

namespace Desk.Core.Queries.Intefaces;

public interface IBaseSortQuery<TEntity> : IBaseQuery<TEntity> where TEntity : class
{
    public string SortBy { get; set; }
    public bool IsAscending { get; set; }

    public abstract Expression<Func<TEntity, object>> GetSortingExpression();
}