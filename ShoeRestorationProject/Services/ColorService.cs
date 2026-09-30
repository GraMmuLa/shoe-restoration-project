using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class ColorService(IRepository<Color, int> repository, IUnitOfWork<Color> unitOfWork, IMapper mapper)
{
    public async Task<ColorDto> AddAsync(ColorDto entity) =>
         mapper.Map<ColorDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<Color>(entity))));

    public ColorDto Update(ColorDto entity) =>
        mapper.Map<ColorDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<Color>(entity))));

    public void Delete(ColorDto entity) =>
        unitOfWork.Execute(() => repository.Delete(mapper.Map<Color>(entity)));

    public async Task<ColorDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<ColorDto>(model);
    }

    public async Task<IList<ColorDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ColorDto>)];
}