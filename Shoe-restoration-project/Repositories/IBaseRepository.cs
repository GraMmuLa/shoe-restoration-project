using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories
{
    public interface IBaseRepository<TModel> 
        where TModel : class
    {
        public void Add(TModel entity);
        public void Update(TModel entity);
        public void Delete(TModel entity);
        public TModel? GetById(object id);
        public IList<TModel> GetAll();
    }
}
