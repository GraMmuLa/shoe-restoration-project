using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;
using ShoeRestorationProject.Features.ShoeMeasurements.DTO;

namespace ShoeRestorationProject.Features.ShoeMeasurements;

public class ShoeMeasurementService(IRepository<ShoeMeasurement, int> repository,
    IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<ShoeMeasurementResponse> AddAsync(ShoeMeasurementRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Id == entity.Value.GetHashCode()))
            throw new UniqueObjectException("MeasurementValue already exists");
        
        var shoeMeasurement = await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<ShoeMeasurement>(entity)));

        return await GetByIdAsync(shoeMeasurement.Id);
    }
    
    public async Task<ShoeMeasurementResponse> UpdateAsync(int id, ShoeMeasurementRequest shoeMeasurementRequest)
    {
        var shoeMeasurement = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("ShoeMeasurement is not found");

        mapper.Map(shoeMeasurementRequest, shoeMeasurement);
        
        return mapper.Map<ShoeMeasurementResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(shoeMeasurement)));
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
