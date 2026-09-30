public class ShoeTypeService(IRepository<ShoeType, int> repository, IUnitOfWork<ShoeType> unitOfWork, IMapper mapper)
{
    public async Task<ShoeTypeDto> AddAsync(ShoeTypeDto entity) =>
         mapper.Map<ShoeTypeDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<ShoeType>(entity))));

    public ShoeTypeDto Update(ShoeTypeDto entity) =>
        mapper.Map<ShoeTypeDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<ShoeType>(entity))));

    public void Delete(ShoeTypeDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<ShoeType>(entity)));

    public async Task<ShoeTypeDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<ShoeTypeDto>(model);
    }

    public async Task<IList<ShoeTypeDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeTypeDto>)];
}