using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public abstract class BaseService<TDto, TModel> : IBaseService<TDto, TModel>
        where TDto : class, IDtoObject<TModel>
        where TModel : class
    {
        private readonly IBaseRepository<TModel> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IBaseRepository<TModel> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual void Add(TDto entity)
        {

            _unitOfWork.Execute(() =>
            {
                _repository.Add(entity.GetModel());
            });
        }

        public virtual void Update(TDto entity)
        {
            _unitOfWork.Execute(() =>
            {
                _repository.Update(entity.GetModel());
            });
        }

        public virtual void Delete(TDto entity)
        {
            _unitOfWork.Execute(() =>
            {
                _repository.Delete(entity.GetModel());
            });
        }

        public virtual IList<TDto> GetAll()
        {
            IList<TModel> models = _repository.GetAll();
            List<TDto> resultDtos = new List<TDto>();
            foreach (TModel model in models)
                resultDtos.Add((TDto)TDto.FromModel(model));
            return resultDtos;
        }

        public virtual TDto? GetById(object id)
        {
            TModel? model = _repository.GetById(id);
            return model is not null ? (TDto)TDto.FromModel(model) : null;
        }
    }
}
