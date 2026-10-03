using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.ShoeTypes;

public class ShoeTypeService(IRepository<ShoeType, int> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ShoeTypeResponse> AddAsync(ShoeTypeRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("ShoeType already exists");
        
        return mapper.Map<ShoeTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<ShoeType>(entity))));
    }    public async Task<ShoeTypeResponse> UpdateAsync(int id, ShoeTypeRequest shoetypeRequest)
    {
        var shoetype = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("ShoeType is not found");

        mapper.Map(shoetypeRequest, shoetype);
        
        return mapper.Map<ShoeTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(shoetype)));
    }


    public async Task<ShoeTypeResponse> DeleteAsync(ShoeTypeResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("ShoeType not found");
            
        return mapper.Map<ShoeTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<ShoeType>(entity))));
    }
    
    public async Task<ShoeTypeResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("ShoeType not found");
            
        return mapper.Map<ShoeTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<ShoeTypeResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<ShoeTypeResponse>(model ??
            throw new NotFoundException("ShoeType not found") );
    }

    public async Task<IList<ShoeTypeResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeTypeResponse>)];
}
