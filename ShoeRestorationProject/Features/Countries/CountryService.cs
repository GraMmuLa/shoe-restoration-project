using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Countries;

public class CountryService(IRepository<Country, int> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<CountryResponse> AddAsync(CountryRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("Country already exists");
        
        return mapper.Map<CountryResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<Country>(entity))));
    }    public async Task<CountryResponse> UpdateAsync(int id, CountryRequest countryRequest)
    {
        var country = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Country is not found");

        mapper.Map(countryRequest, country);
        
        return mapper.Map<CountryResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(country)));
    }


    public async Task<CountryResponse> DeleteAsync(CountryRequest entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Name == entity.Name))
            throw new NotFoundException("Country not found");
            
        return mapper.Map<CountryResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<Country>(entity))));
    }
    
    public async Task<CountryResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("Country not found");
            
        return mapper.Map<CountryResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<CountryResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<CountryResponse>(model ??
                                           throw new NotFoundException("Country not found") );
    }

    public async Task<IList<CountryResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<CountryResponse>)];
}