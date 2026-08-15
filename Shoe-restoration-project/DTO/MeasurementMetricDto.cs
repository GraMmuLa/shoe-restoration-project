using Shoe_restoration_project.Models;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace Shoe_restoration_project.DTO
{
    public class MeasurementMetricDto : IDtoObject<MeasurementMetric>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public MeasurementMetric GetModel()
        {
            return new MeasurementMetric()
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public static IDtoObject<MeasurementMetric> FromModel(MeasurementMetric measurementMetric)
        {
            return new MeasurementMetricDto()
            {
                Id = measurementMetric.Id,
                Name = measurementMetric.Name
            };
        }
    }
}
