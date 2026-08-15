using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class MeasurementValuesService : BaseService<MeasurementValueDto, MeasurementValue>, IMeasurementValuesService
    {
        private readonly IMeasurementValuesRepository _measurementValuesRepository;

        public MeasurementValuesService(IMeasurementValuesRepository measurementValuesRepository, IUnitOfWork unitOfWork)
            : base(measurementValuesRepository, unitOfWork)
        {
            _measurementValuesRepository = measurementValuesRepository;
        }
    }
}
