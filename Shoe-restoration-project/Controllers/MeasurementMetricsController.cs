using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class MeasurementMetricsController : BaseApiController<MeasurementMetricDto, MeasurementMetric>
    {
        private readonly IMeasurementMetricsService _measurementMetricsService;

        public MeasurementMetricsController(IMeasurementMetricsService measurementMetricsService)
            : base(measurementMetricsService)
        {
            _measurementMetricsService = measurementMetricsService;
        }
    }
}
