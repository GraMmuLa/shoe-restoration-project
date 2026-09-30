public class MeasurementValueService(IRepository<MeasurementValue, int> repository, IUnitOfWork<MeasurementValue> unitOfWork, IMapper mapper)
{
    public async Task<MeasurementValueDto> AddAsync(MeasurementValueDto entity) =>
         mapper.Map<MeasurementValueDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<MeasurementValue>(entity))));

    public MeasurementValueDto Update(MeasurementValueDto entity) =>
        mapper.Map<MeasurementValueDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<MeasurementValue>(entity))));

    public void Delete(MeasurementValueDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<MeasurementValue>(entity)));

    public async Task<MeasurementValueDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<MeasurementValueDto>(model);
    }

    public async Task<IList<MeasurementValueDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<MeasurementValueDto>)];
}