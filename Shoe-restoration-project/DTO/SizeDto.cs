using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class SizeDto : IDtoObject<Size>
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int SizeMetricId { get; set; }

        public Size GetModel()
        {
            return new Size()
            {
                Id = this.Id,
                Value = this.Value,
                SizeMetricId = this.SizeMetricId
            };
        }

        public static IDtoObject<Size> FromModel(Size size)
        {
            return new SizeDto()
            {
                Id = size.Id,
                Value = size.Value,
                SizeMetricId = size.SizeMetricId
            };
        }
    }
}
