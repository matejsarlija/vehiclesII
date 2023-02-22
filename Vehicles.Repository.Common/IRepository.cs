using System.Linq.Expressions;

namespace Vehicles.Repository.Common;

public interface IRepository<T> where T : class
{
    Task<IQueryable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");

    Task<T> GetByIdAsync(object id);

    Task InsertAsync(T entity); 

    Task DeleteAsync(object id);

    void Delete(T entity);

    void Update(T entity);
}