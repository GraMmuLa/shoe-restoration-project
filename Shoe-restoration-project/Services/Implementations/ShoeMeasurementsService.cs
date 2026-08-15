using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class ShoeMeasurementsService : BaseService<ShoeMeasurementDto, ShoeMeasurement>, IShoeMeasurementsService
    {
        private readonly IShoeMeasurementsRepository _shoeMeasurementsRepository;
        public ShoeMeasurementsService(IShoeMeasurementsRepository shoeMeasurementsRepository, IUnitOfWork unitOfWork)
            : base(shoeMeasurementsRepository, unitOfWork)
        {
            _shoeMeasurementsRepository = shoeMeasurementsRepository;
        }
    }
}
