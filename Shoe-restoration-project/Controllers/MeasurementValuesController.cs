using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class MeasurementValuesController : BaseApiController<MeasurementValueDto, MeasurementValue>
    {
        private readonly IMeasurementValuesService _measurementValuesService;

        public MeasurementValuesController(IMeasurementValuesService measurementValuesService)
            : base(measurementValuesService)
        {
            _measurementValuesService = measurementValuesService;
        }
    }
}
