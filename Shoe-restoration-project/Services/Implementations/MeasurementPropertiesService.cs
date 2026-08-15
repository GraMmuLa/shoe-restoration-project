using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class MeasurementPropertiesService : BaseService<MeasurementPropertyDto, MeasurementProperty>, IMeasurementPropertiesService
    {
        private readonly IMeasurementPropertiesRepository _measurementPropertiesRepository;

        public MeasurementPropertiesService(IMeasurementPropertiesRepository measurementPropertiesRepository, IUnitOfWork unitOfWork)
            : base(measurementPropertiesRepository, unitOfWork)
        {
            _measurementPropertiesRepository = measurementPropertiesRepository;
        }
    }
}
