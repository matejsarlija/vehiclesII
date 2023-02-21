using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vehicles.Service.Data;

namespace Vehicles.Repository.Common;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly VehicleContext _context;

    public GenericRepository(VehicleContext context)
    {
        _dbSet = context.Set<T>();
        _context = context;
    }

    public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<T> GetById(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task DeleteAsync(object id)
    {
        T entityToDelete = _dbSet.Find(id);
        Delete(entityToDelete);
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}
