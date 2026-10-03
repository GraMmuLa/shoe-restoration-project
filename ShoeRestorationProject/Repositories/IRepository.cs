using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShoeRestorationProject.Repositories;

public interface IRepository<T, TKey> where T : class
{
    Task<T> AddAsync(T entity);
    T Update(T entity);
    T Delete(T entity);
    Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByPropertyAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
    Task<bool> ExistsByPropertyAsync(Expression<Func<T, bool>> obj);
}