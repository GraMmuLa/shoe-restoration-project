using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class ShoeDto : IDtoObject<Shoe>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ConditionId { get; set; }
        public int BrandId { get; set; }
        public int SkinTypeId { get; set; }
        public int SizeId { get; set; }
        public int ShoeTypeId { get; set; }

        public Shoe GetModel()
        {
            return new Shoe()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                ConditionId = this.ConditionId,
                BrandId = this.BrandId,
                SkinTypeId = this.SkinTypeId,
                SizeId = this.SizeId,
                ShoeTypeId = this.ShoeTypeId
            };
        }

        public static IDtoObject<Shoe> FromModel(Shoe shoe)
        {
            return new ShoeDto()
            {
                Id = shoe.Id,
                Title = shoe.Title,
                Description = shoe.Description,
                ConditionId = shoe.ConditionId,
                BrandId = shoe.BrandId,
                SkinTypeId = shoe.SkinTypeId,
                SizeId = shoe.SizeId,
                ShoeTypeId = shoe.ShoeTypeId
            };
        }
    }
}
