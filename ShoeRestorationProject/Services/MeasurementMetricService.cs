using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class MeasurementMetricService(IRepository<MeasurementMetric, int> repository, IUnitOfWork<MeasurementMetric> unitOfWork, IMapper mapper)
{
    public async Task<MeasurementMetricDto> AddAsync(MeasurementMetricDto entity) =>
         mapper.Map<MeasurementMetricDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<MeasurementMetric>(entity))));

    public MeasurementMetricDto Update(MeasurementMetricDto entity) =>
        mapper.Map<MeasurementMetricDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<MeasurementMetric>(entity))));

    public void Delete(MeasurementMetricDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<MeasurementMetric>(entity)));

    public async Task<MeasurementMetricDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<MeasurementMetricDto>(model);
    }

    public async Task<IList<MeasurementMetricDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<MeasurementMetricDto>)];
}