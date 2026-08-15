using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class MeasurementPropertyDto : IDtoObject<MeasurementProperty>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public MeasurementProperty GetModel()
        {
            return new MeasurementProperty()
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public static IDtoObject<MeasurementProperty> FromModel(MeasurementProperty measurementProperty)
        {
            return new MeasurementPropertyDto()
            {
                Id = measurementProperty.Id,
                Name = measurementProperty.Name
            };
        }
    }
}
