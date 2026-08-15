using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Services
{
    public interface IBaseService<TDto, TModel>
        where TDto : class, IDtoObject<TModel>
        where TModel : class
    {
        public void Add(TDto entity);
        public void Delete(TDto entity);
        public void Update(TDto entity);
        public TDto? GetById(object id);
        public IList<TDto> GetAll();
    }
}
