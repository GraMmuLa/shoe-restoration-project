using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class ShoeService(IShoesRepository repository, IUnitOfWork<Shoe> unitOfWork, IMapper mapper) 
{
    public async Task<ShoeDto> AddAsync(ShoeDto entity) =>
         mapper.Map<ShoeDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<Shoe>(entity))));

    public ShoeDto Update(ShoeDto entity) =>
        mapper.Map<ShoeDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<Shoe>(entity))));

    public void Delete(ShoeDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<Shoe>(entity)));

    public async Task<ShoeDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<ShoeDto>(model);
    }

    public async Task<IList<ShoeDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeDto>)];
}
