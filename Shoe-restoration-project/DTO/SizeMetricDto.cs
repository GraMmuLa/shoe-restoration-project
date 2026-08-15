using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class SizeMetricDto : IDtoObject<SizeMetric>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public SizeMetric GetModel()
        {
            return new SizeMetric()
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public static IDtoObject<SizeMetric> FromModel(SizeMetric sizeMetric)
        {
            return new SizeMetricDto()
            {
                Id = sizeMetric.Id,
                Name = sizeMetric.Name
            };
        }
    }
}
