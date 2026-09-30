using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class BrandService(IRepository<Brand, int> repository,
    IUnitOfWork<Brand> unitOfWork,
    IMapper mapper)
{
    public async Task<BrandDto> AddAsync(BrandDto entity) =>
         mapper.Map<BrandDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<Brand>(entity))));

    public BrandDto Update(BrandDto entity) =>
        mapper.Map<BrandDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<Brand>(entity))));

    public void Delete(BrandDto entity) =>
        unitOfWork.Execute(() => repository.Delete(mapper.Map<Brand>(entity)));

    public async Task<BrandDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<BrandDto>(model);
    }

    public async Task<IList<BrandDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<BrandDto>)];
}