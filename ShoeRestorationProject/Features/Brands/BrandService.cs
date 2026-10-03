using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Brands;

public class BrandService(IRepository<Brand, int> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
{
    public async Task<BrandResponse> AddAsync(BrandRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("Brand already exists");
        
        return mapper.Map<BrandResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<Brand>(entity))));
    }    public async Task<BrandResponse> UpdateAsync(int id, BrandRequest brandRequest)
    {
        var brand = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Brand is not found");

        mapper.Map(brandRequest, brand);
        
        return mapper.Map<BrandResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(brand)));
    }


    public async Task<BrandResponse> DeleteAsync(BrandRequest entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Name == entity.Name))
            throw new NotFoundException("Brand not found");
            
        return mapper.Map<BrandResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<Brand>(entity))));
    }
    
    public async Task<BrandResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("Brand not found");
            
        return mapper.Map<BrandResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<BrandResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id,
            x=>x.Country);
        
        return mapper.Map<BrandResponse>(model ??
            throw new NotFoundException("Brand not found") );
    }

    public async Task<IList<BrandResponse>> GetAllAsync()
    {
        var result = await repository.GetAllAsync(x => x.Country);
        
        return [..(result)
            .Select(mapper.Map<BrandResponse>)];
    }

}