using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class SizeMetricsController : BaseApiController<SizeMetricDto, SizeMetric>
    {
        private readonly ISizeMetricsService _sizeMetricsService;

        public SizeMetricsController(ISizeMetricsService sizeMetricsService)
            : base(sizeMetricsService)
        {
            _sizeMetricsService = sizeMetricsService;
        }
    }
}
