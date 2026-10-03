using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoeRestorationProject.Context;

namespace ShoeRestorationProject.Repositories.Implementations;

public class Repository<T, TKey> : IRepository<T, TKey> where T : class
{
    protected AppDbContext DbContext { get; }
    private DbSet<T> DbSet { get; }

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    public async Task<T> AddAsync(T entity) => (await DbSet.AddAsync(entity)).Entity;

    public T Update(T entity) => DbSet.Update(entity).Entity;

    public T Delete(T entity) => (DbSet.Remove(entity)).Entity;

    public async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        foreach (var include in includes ?? [])
            query = query.Include(include);
        
        return await query.ToListAsync();   
    }

    public async Task<T?> GetByPropertyAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;
        
        foreach (var include in includes ?? [])
            query = query.Include(include);
        
        return await query.FirstOrDefaultAsync(predicate);
    }
    
    public async Task<bool> ExistsByPropertyAsync(Expression<Func<T, bool>> obj) =>
        await DbSet.AnyAsync(obj);
}