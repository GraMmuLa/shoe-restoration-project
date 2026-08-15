using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    //TODO
    public class ShoeMeasurementDto : IDtoObject<ShoeMeasurement>
    {
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public int MeasurementPropertyId { get; set; }
        public int MeasurementValueId { get; set; }

        public ShoeMeasurement GetModel()
        {
            return new ShoeMeasurement()
            {
                Id = this.Id,
                ShoeId = this.ShoeId,
                MeasurementPropertyId = this.MeasurementPropertyId,
                MeasurementValueId = this.MeasurementValueId
            };
        }

        public static IDtoObject<ShoeMeasurement> FromModel(ShoeMeasurement model)
        {
            return new ShoeMeasurementDto
            {
                Id = model.Id,
                ShoeId = model.ShoeId,
                MeasurementPropertyId = model.MeasurementPropertyId,
                MeasurementValueId = model.MeasurementValueId,
            };
        }
    }
}
