using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.SizeMetrics;

public class SizeMetricService(IRepository<SizeMetric, int> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<SizeMetricResponse> AddAsync(SizeMetricRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("SizeMetric already exists");
        
        return mapper.Map<SizeMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<SizeMetric>(entity))));
    }    public async Task<SizeMetricResponse> UpdateAsync(int id, SizeMetricRequest sizemetricRequest)
    {
        var sizemetric = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("SizeMetric is not found");

        mapper.Map(sizemetricRequest, sizemetric);
        
        return mapper.Map<SizeMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(sizemetric)));
    }


    public async Task<SizeMetricResponse> DeleteAsync(SizeMetricResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("SizeMetric not found");
            
        return mapper.Map<SizeMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<SizeMetric>(entity))));
    }
    
    public async Task<SizeMetricResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("SizeMetric not found");
            
        return mapper.Map<SizeMetricResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<SizeMetricResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<SizeMetricResponse>(model ??
            throw new NotFoundException("SizeMetric not found") );
    }

    public async Task<IList<SizeMetricResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<SizeMetricResponse>)];
}
