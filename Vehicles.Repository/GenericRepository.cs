using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vehicles.DAL.Data;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly VehicleContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(VehicleContext context)
    {
        _dbSet = context.Set<T>();
        _context = context;
    }

    public async Task<IQueryable<T>> Get(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null) query = query.Where(filter);

        foreach (var includeProperty in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy != null) query = orderBy(query);

        return await Task.FromResult(query);
    }

    public async Task<T> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task DeleteAsync(object id)
    {
        var entityToDelete = _dbSet.Find(id);
        Delete(entityToDelete);
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}