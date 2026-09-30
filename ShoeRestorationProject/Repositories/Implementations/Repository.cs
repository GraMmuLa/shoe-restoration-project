using Microsoft.EntityFrameworkCore;
using ShoeRestorationProject.Context;

namespace ShoeRestorationProject.Repositories.Implementations;

public class Repository<T, TKey> : IRepository<T, TKey> where T : class
{
    protected AppDbContext DbContext { get; }
    protected DbSet<T> DbSet { get; }

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    public T Add(T entity) => DbSet.Add(entity).Entity;

    public T Update(T entity) => DbSet.Update(entity).Entity;

    public void Delete(T entity) => DbSet.Remove(entity);

    public virtual async Task<T?> GetByIdAsync(TKey id) => await DbSet.FindAsync(id);

    public async Task<IList<T>> GetAllAsync() => await DbSet.ToListAsync();
}