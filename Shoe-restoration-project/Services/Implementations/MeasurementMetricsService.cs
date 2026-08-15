using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class MeasurementMetricsService : BaseService<MeasurementMetricDto, MeasurementMetric>, IMeasurementMetricsService
    {
        private readonly IMeasurementMetricsRepository _measurementMetricsRepository;

        public MeasurementMetricsService(IMeasurementMetricsRepository measurementMetricsRepository, IUnitOfWork unitOfWork)
            : base(measurementMetricsRepository, unitOfWork)
        {
            _measurementMetricsRepository = measurementMetricsRepository;
        }
    }
}
