using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Sizes;

public class SizeService(IRepository<Size, int> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<SizeResponse> AddAsync(SizeRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Id == 0))
            throw new UniqueObjectException("Size already exists");
        
        return mapper.Map<SizeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<Size>(entity))));
    }
    
    public async Task<SizeResponse> UpdateAsync(int id, SizeRequest sizeRequest)
    {
        var size = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Size is not found");

        mapper.Map(sizeRequest, size);
        
        return mapper.Map<SizeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(size)));
    }
    
    public async Task<SizeResponse> DeleteAsync(SizeResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("Size not found");
            
        return mapper.Map<SizeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<Size>(entity))));
    }
    
    public async Task<SizeResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("Size not found");
            
        return mapper.Map<SizeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<SizeResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<SizeResponse>(model ??
            throw new NotFoundException("Size not found") );
    }

    public async Task<IList<SizeResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<SizeResponse>)];
}
