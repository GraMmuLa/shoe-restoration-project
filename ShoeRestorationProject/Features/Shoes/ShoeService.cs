using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;
using ShoeRestorationProject.Features.Shoes;

namespace ShoeRestorationProject.Services;

public class ShoeService(IShoesRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ShoeResponse> AddAsync(ShoeRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Id == 0))
            throw new UniqueObjectException("Shoe already exists");
        
        return mapper.Map<ShoeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<Shoe>(entity))));
    }    public async Task<ShoeResponse> UpdateAsync(int id, ShoeRequest shoeRequest)
    {
        var shoe = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Shoe is not found");

        mapper.Map(shoeRequest, shoe);
        
        return mapper.Map<ShoeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(shoe)));
    }


    public async Task<ShoeResponse> DeleteAsync(ShoeResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("Shoe not found");
            
        return mapper.Map<ShoeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<Shoe>(entity))));
    }
    
    public async Task<ShoeResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("Shoe not found");
            
        return mapper.Map<ShoeResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<ShoeResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<ShoeResponse>(model ??
            throw new NotFoundException("Shoe not found") );
    }

    public async Task<IList<ShoeResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeResponse>)];
}
