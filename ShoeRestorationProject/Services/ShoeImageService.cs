using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class ShoeImageService(IRepository<ShoeImage, Guid> repository, IUnitOfWork<ShoeImage> unitOfWork, IMapper mapper)
{
    public async Task<ShoeImageDto> AddAsync(ShoeImageDto entity) =>
         mapper.Map<ShoeImageDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<ShoeImage>(entity))));

    public ShoeImageDto Update(ShoeImageDto entity) =>
        mapper.Map<ShoeImageDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<ShoeImage>(entity))));

    public void Delete(ShoeImageDto entity) =>
        unitOfWork.Execute(() => repository.Delete(mapper.Map<ShoeImage>(entity)));

    public async Task<ShoeImageDto?> GetByIdAsync(Guid id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<ShoeImageDto>(model);
    }

    public async Task<IList<ShoeImageDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeImageDto>)];
}