using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class SizeMetricsService : BaseService<SizeMetricDto, SizeMetric>, ISizeMetricsService
    {
        private readonly ISizeMetricsRepository _sizeMetricsRepository;

        public SizeMetricsService(ISizeMetricsRepository sizeMetricsRepository, IUnitOfWork unitOfWork)
            : base(sizeMetricsRepository, unitOfWork)
        {
            _sizeMetricsRepository = sizeMetricsRepository;
        }
    }
}
