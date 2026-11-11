using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;
using ShoeRestorationProject.Features.MeasurementMetrics.DTO;

namespace ShoeRestorationProject.Features.MeasurementMetrics;

public class MeasurementMetricService(
    IRepository<MeasurementMetric, int> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
{
    public async Task<MeasurementMetricResponse> AddAsync(MeasurementMetricRequest entity)
    {
        if (await repository.ExistsByPropertyAsync(x => x.Name == entity.Name))
            throw new UniqueObjectException("MeasurementMetric already exists");

        var measurementMetric = await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<MeasurementMetric>(entity)));

        return await GetByIdAsync(measurementMetric.Id);
    }

    public async Task<MeasurementMetricResponse> UpdateAsync(int id, MeasurementMetricRequest measurementMetricRequest)
    {
        var measurementMetric = await repository.GetByPropertyAsync(x => x.Id == id) ??
            throw new NotFoundException("MeasurementMetric is not found");

        mapper.Map(measurementMetricRequest, measurementMetric);

        return mapper.Map<MeasurementMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(measurementMetric)));
    }


    public async Task<MeasurementMetricResponse> DeleteAsync(MeasurementMetricResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("MeasurementMetric not found");

        return mapper.Map<MeasurementMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<MeasurementMetric>(entity))));
    }

    public async Task<MeasurementMetricResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x => x.Id == id) ??
            throw new NotFoundException("MeasurementMetric not found");

        return mapper.Map<MeasurementMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<MeasurementMetricResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x => x.Id == id);

        return mapper.Map<MeasurementMetricResponse>(model ??
            throw new NotFoundException("MeasurementMetric not found"));
    }

    public async Task<IList<MeasurementMetricResponse>> GetAllAsync() =>
        [.. (await repository.GetAllAsync()).Select(mapper.Map<MeasurementMetricResponse>)];
}