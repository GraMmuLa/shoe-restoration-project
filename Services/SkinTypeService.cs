public class SkinTypeService(IRepository<SkinType, int> repository, IUnitOfWork<SkinType> unitOfWork, IMapper mapper)
{
    public async Task<SkinTypeDto> AddAsync(SkinTypeDto entity) =>
         mapper.Map<SkinTypeDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<SkinType>(entity))));

    public SkinTypeDto Update(SkinTypeDto entity) =>
        mapper.Map<SkinTypeDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<SkinType>(entity))));

    public void Delete(SkinTypeDto entity) => unitOfWork.Execute(() => repository.Delete(mapper.Map<SkinType>(entity)));

    public async Task<SkinTypeDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<SkinTypeDto>(model);
    }

    public async Task<IList<SkinTypeDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<SkinTypeDto>)];
}