using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Conditions;

public class ConditionService(IRepository<Condition, int> repository,
    IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ConditionResponse> AddAsync(ConditionRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("Condition already exists");
        
        return mapper.Map<ConditionResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<Condition>(entity))));
    }    public async Task<ConditionResponse> UpdateAsync(int id, ConditionRequest conditionRequest)
    {
        var condition = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Condition is not found");

        mapper.Map(conditionRequest, condition);
        
        return mapper.Map<ConditionResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(condition)));
    }


    public async Task<ConditionResponse> DeleteAsync(ConditionRequest entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Name == entity.Name))
            throw new NotFoundException("Condition not found");
            
        return mapper.Map<ConditionResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<Condition>(entity))));
    }
    
    public async Task<ConditionResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("Condition not found");
            
        return mapper.Map<ConditionResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<ConditionResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<ConditionResponse>(model ??
            throw new NotFoundException("Condition not found") );
    }

    public async Task<IList<ConditionResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ConditionResponse>)];
}
