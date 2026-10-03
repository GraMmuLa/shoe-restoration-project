using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Colors;

public class ColorService(IRepository<Color, int> repository,
    IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ColorResponse> AddAsync(ColorRequest colorRequest)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == colorRequest.Name))
            throw new UniqueObjectException("Color already exists");
        
        return mapper.Map<ColorResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<Color>(colorRequest))));
    }

    public async Task<ColorResponse> UpdateAsync(int id, ColorRequest colorRequest)
    {
        var color = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Color is not found");

        mapper.Map(colorRequest, color);
        
        return mapper.Map<ColorResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(color)));
    }

    public async Task<ColorResponse> DeleteAsync(ColorRequest colorRequest)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Name == colorRequest.Name))
            throw new NotFoundException("Color not found");
            
        return mapper.Map<ColorResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<Color>(colorRequest))));
    }
    
    public async Task<ColorResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("Color not found");
            
        return mapper.Map<ColorResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<ColorResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<ColorResponse>(model ??
            throw new NotFoundException("Color not found") );
    }

    public async Task<IList<ColorResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ColorResponse>)];
}
