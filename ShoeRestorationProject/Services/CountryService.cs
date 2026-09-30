using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Models;

using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class CountryService(IRepository<Country, int> repository, IUnitOfWork<Country> unitOfWork, IMapper mapper)
{
    public async Task<CountryDto> AddAsync(CountryDto entity) =>
         mapper.Map<CountryDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<Country>(entity))));

    public CountryDto Update(CountryDto entity) =>
        mapper.Map<CountryDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<Country>(entity))));

    public void Delete(CountryDto entity) =>
        unitOfWork.Execute(() => repository.Delete(mapper.Map<Country>(entity)));

    public async Task<CountryDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<CountryDto>(model);
    }

    public async Task<IList<CountryDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<CountryDto>)];
}