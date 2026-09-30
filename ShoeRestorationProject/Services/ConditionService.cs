using MapsterMapper;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Services;

public class ConditionService(IRepository<Condition, int> repository, IUnitOfWork<Condition> unitOfWork, IMapper mapper)
{
    public async Task<ConditionDto> AddAsync(ConditionDto entity) =>
         mapper.Map<ConditionDto>(await unitOfWork.ExecuteAsync(() =>
             repository.Add(mapper.Map<Condition>(entity))));

    public ConditionDto Update(ConditionDto entity) =>
        mapper.Map<ConditionDto>(unitOfWork.Execute(() =>
            repository.Update(mapper.Map<Condition>(entity))));

    public void Delete(ConditionDto entity) =>
        unitOfWork.Execute(() => repository.Delete(mapper.Map<Condition>(entity)));

    public async Task<ConditionDto?> GetByIdAsync(int id)
    {
        var model = await repository.GetByIdAsync(id);
        return model is null ? null : mapper.Map<ConditionDto>(model);
    }

    public async Task<IList<ConditionDto>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ConditionDto>)];
}