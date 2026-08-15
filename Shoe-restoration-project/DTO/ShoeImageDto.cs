using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class ShoeImageDto : IDtoObject<ShoeImage>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? ImageData { get; set; } = null!;
        public int ShoeId { get; set; }

        public ShoeImage GetModel()
        {
            return new ShoeImage()
            {
                Id = this.Id ?? Guid.Empty,
                Name = this.Name,
                ImageData = this.ImageData,
                ShoeId = this.ShoeId
            };
        }

        public static IDtoObject<ShoeImage> FromModel(ShoeImage shoeImage)
        {
            return new ShoeImageDto()
            {
                Id = shoeImage.Id,
                Name = shoeImage.Name,
                ShoeId = shoeImage.ShoeId
            };
        }

        //TODO
        public void LoadImage(ShoeImage shoeImage)
        {

        }
    }
}
