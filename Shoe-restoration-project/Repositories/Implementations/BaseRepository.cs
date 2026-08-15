using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel>
        where TModel : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TModel> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TModel>();
        }

        public virtual void Add(TModel entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(TModel entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual IList<TModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public TModel? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Update(TModel entity)
        {
            _dbSet.Update(entity);
        }
    }
}
