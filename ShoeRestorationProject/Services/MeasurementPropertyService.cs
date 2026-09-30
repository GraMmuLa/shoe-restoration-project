using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class MeasurementPropertyService(IRepository<MeasurementProperty, int> repository, IUnitOfWork<MeasurementProperty> unitOfWork, IMapper mapper)
{
    public async Task<MeasurementPropertyDto> AddAsync(MeasurementPropertyDto entity) =>
         mapper.Map<MeasurementPropertyDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<MeasurementProperty>(entity))));

    public MeasurementPropertyDto Update(MeasurementPropertyDto entity) =>
        mapper.Map<MeasurementPropertyDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<MeasurementProperty>(entity))));

    public void Delete(MeasurementPropertyDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<MeasurementProperty>(entity)));

    public async Task<MeasurementPropertyDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<MeasurementPropertyDto>(model);
    }

    public async Task<IList<MeasurementPropertyDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<MeasurementPropertyDto>)];
}