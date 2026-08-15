using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class MeasurementValueDto : IDtoObject<MeasurementValue>
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public int MeasurementMetricId { get; set; }

        public MeasurementValue GetModel()
        {
            return new MeasurementValue()
            {
                Id = this.Id,
                Value = this.Value,
                MeasurementMetricId = this.MeasurementMetricId
            };
        }

        public static IDtoObject<MeasurementValue> FromModel(MeasurementValue measurementValue)
        {
            return new MeasurementValueDto()
            {
                Id = measurementValue.Id,
                Value = measurementValue.Value,
                MeasurementMetricId = measurementValue.MeasurementMetricId
            };
        }
    }
}
