using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.ShoeMeasurements;

public class ShoeMeasurementService(IRepository<ShoeMeasurement, int> repository,
    IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ShoeMeasurementResponse> AddAsync(ShoeMeasurementRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Id == entity.Value.GetHashCode()))
            throw new UniqueObjectException("MeasurementValue already exists");
        
        return mapper.Map<ShoeMeasurementResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<ShoeMeasurement>(entity))));
    }    public async Task<ShoeMeasurementResponse> UpdateAsync(int id, ShoeMeasurementRequest shoemeasurementRequest)
    {
        var shoemeasurement = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("ShoeMeasurement is not found");

        mapper.Map(shoemeasurementRequest, shoemeasurement);
        
        return mapper.Map<ShoeMeasurementResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(shoemeasurement)));
    }


    public async Task<ShoeMeasurementResponse> DeleteAsync(ShoeMeasurementResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("MeasurementValue not found");
            
        return mapper.Map<ShoeMeasurementResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<ShoeMeasurement>(entity))));
    }
    
    public async Task<ShoeMeasurementResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("MeasurementValue not found");
            
        return mapper.Map<ShoeMeasurementResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<ShoeMeasurementResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<ShoeMeasurementResponse>(model ??
            throw new NotFoundException("MeasurementValue not found") );
    }

    public async Task<IList<ShoeMeasurementResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<ShoeMeasurementResponse>)];
}
