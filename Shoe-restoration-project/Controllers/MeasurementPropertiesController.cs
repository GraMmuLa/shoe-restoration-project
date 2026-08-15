using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class MeasurementPropertiesController : BaseApiController<MeasurementPropertyDto, MeasurementProperty>
    {
        private readonly IMeasurementPropertiesService _measurementMetricsController;

        public MeasurementPropertiesController(IMeasurementPropertiesService measurementMetricsService)
            : base(measurementMetricsService)
        {
            _measurementMetricsController = measurementMetricsService;
        }
    }
}
