using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class SizeService(IRepository<Size, int> repository, IUnitOfWork<Size> unitOfWork, IMapper mapper) 
{
    public async Task<SizeDto> AddAsync(SizeDto entity) =>
         mapper.Map<SizeDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<Size>(entity))));

    public SizeDto Update(SizeDto entity) =>
        mapper.Map<SizeDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<Size>(entity))));

    public void Delete(SizeDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<Size>(entity)));

    public async Task<SizeDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<SizeDto>(model);
    }

    public async Task<IList<SizeDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<SizeDto>)];
}
