using System.Linq.Expressions;

namespace Vehicles.Repository.Common;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

    Task<T> GetById(object id);

    Task InsertAsync(T entity); 

    Task DeleteAsync(object id);

    void Delete(T entity);

    void Update(T entity);
}