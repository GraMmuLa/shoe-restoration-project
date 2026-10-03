using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.SkinTypes;

public class SkinTypeService(IRepository<SkinType, int> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<SkinTypeResponse> AddAsync(SkinTypeRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("SkinType already exists");
        
        return mapper.Map<SkinTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<SkinType>(entity))));
    }
    public async Task<SkinTypeResponse> UpdateAsync(int id, SkinTypeRequest skinTypeRequest)
    {
        var skinType = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("SkinType is not found");

        mapper.Map(skinTypeRequest, skinType);
        
        return mapper.Map<SkinTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(skinType)));
    }


    public async Task<SkinTypeResponse> DeleteAsync(SkinTypeRequest entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Name == entity.Name))
            throw new NotFoundException("SkinType not found");
            
        return mapper.Map<SkinTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<SkinType>(entity))));
    }
    
    public async Task<SkinTypeResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=> x.Id == id) ??
                    throw new NotFoundException("SkinType not found");
            
        return mapper.Map<SkinTypeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<SkinTypeResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<SkinTypeResponse>(model ??
            throw new NotFoundException("SkinType not found") );
    }

    public async Task<IList<SkinTypeResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<SkinTypeResponse>)];
}
