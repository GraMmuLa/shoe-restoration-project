using System.Collections.Generic;

namespace ShoeRestorationProject.Repositories;

public interface IRepository<T, TKey> where T : class
{
    T Add(T entity);
    T Update(T entity);
    void Delete(T entity);
    Task<T?> GetByIdAsync(TKey id);
    Task<IList<T>> GetAllAsync();
}