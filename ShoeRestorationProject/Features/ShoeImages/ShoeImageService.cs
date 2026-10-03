using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.ShoeImages;

public class ShoeImageService(IRepository<ShoeImage, Guid> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ShoeImageResponse> AddAsync(ShoeImageRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Id == Guid.Empty))
            throw new UniqueObjectException("ShoeImage already exists");
        
        return mapper.Map<ShoeImageResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<ShoeImage>(entity))));
    }    public async Task<ShoeImageResponse> UpdateAsync(Guid id, ShoeImageRequest shoeimageRequest)
    {
        var shoeimage = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("ShoeImage is not found");

        mapper.Map(shoeimageRequest, shoeimage);
        
        return mapper.Map<ShoeImageResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(shoeimage)));
    }


    public async Task<ShoeImageResponse> DeleteAsync(ShoeImageResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("ShoeImage not found");
            
        return mapper.Map<ShoeImageResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<ShoeImage>(entity))));
    }
    
    public async Task<ShoeImageResponse> DeleteAsync(Guid id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("ShoeImage not found");
            
        return mapper.Map<ShoeImageResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<ShoeImageResponse> GetByIdAsync(Guid id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<ShoeImageResponse>(model ??
            throw new NotFoundException("ShoeImage not found") );
    }

    public async Task<IList<ShoeImageResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeImageResponse>)];
}
