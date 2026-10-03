using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.MeasurementProperties;

public class MeasurementPropertyService(IRepository<MeasurementProperty, int> repository, IUnitOfWork unitOfWork, IMapper mapper)
{
    public async Task<MeasurementPropertyResponse> AddAsync(MeasurementPropertyRequest entity)
    {
        if(await repository.ExistsByPropertyAsync(x=> x.Name == entity.Name))
            throw new UniqueObjectException("MeasurementProperty already exists");
        
        return mapper.Map<MeasurementPropertyResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.AddAsync(mapper.Map<MeasurementProperty>(entity))));
    }    public async Task<MeasurementPropertyResponse> UpdateAsync(int id, MeasurementPropertyRequest measurementpropertyRequest)
    {
        var measurementproperty = await repository.GetByPropertyAsync(x => x.Id == id)
                    ?? throw new NotFoundException("MeasurementProperty is not found");

        mapper.Map(measurementpropertyRequest, measurementproperty);
        
        return mapper.Map<MeasurementPropertyResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Update(measurementproperty)));
    }


    public async Task<MeasurementPropertyResponse> DeleteAsync(MeasurementPropertyResponse entity)
    {
        if (!await repository.ExistsByPropertyAsync(x => x.Id == entity.Id))
            throw new NotFoundException("MeasurementProperty not found");
            
        return mapper.Map<MeasurementPropertyResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(mapper.Map<MeasurementProperty>(entity))));
    }
    
    public async Task<MeasurementPropertyResponse> DeleteAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id) ??
                    throw new NotFoundException("MeasurementProperty not found");
            
        return mapper.Map<MeasurementPropertyResponse>(await unitOfWork.ExecuteAsync(() =>
            repository.Delete(model)));
    }

    public async Task<MeasurementPropertyResponse> GetByIdAsync(int id)
    {
        var model = await repository.GetByPropertyAsync(x=>x.Id == id);
        
        return mapper.Map<MeasurementPropertyResponse>(model ??
            throw new NotFoundException("MeasurementProperty not found") );
    }

    public async Task<IList<MeasurementPropertyResponse>> GetAllAsync() =>
        [..(await repository.GetAllAsync()).Select(mapper.Map<MeasurementPropertyResponse>)];
}
