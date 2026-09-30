public class SizeMetricService(IRepository<SizeMetric, int> repository, IUnitOfWork<SizeMetric> unitOfWork, IMapper mapper)
{
    public async Task<SizeMetricDto> AddAsync(SizeMetricDto entity) =>
         mapper.Map<SizeMetricDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<SizeMetric>(entity))));

    public SizeMetricDto Update(SizeMetricDto entity) =>
        mapper.Map<SizeMetricDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<SizeMetric>(entity))));

    public void Delete(SizeMetricDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<SizeMetric>(entity)));

    public async Task<SizeMetricDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<SizeMetricDto>(model);
    }

    public async Task<IList<SizeMetricDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<SizeMetricDto>)];
}