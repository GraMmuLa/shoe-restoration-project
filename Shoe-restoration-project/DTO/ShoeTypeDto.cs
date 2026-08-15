using Shoe_restoration_project.Models;
using System.Runtime.CompilerServices;

namespace Shoe_restoration_project.DTO
{
    public class ShoeTypeDto : IDtoObject<ShoeType>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ShoeType GetModel()
        {
            return new ShoeType()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            };
        }

        public static IDtoObject<ShoeType> FromModel(ShoeType shoeType)
        {
            return new ShoeTypeDto()
            {
                Id = shoeType.Id,
                Name = shoeType.Name,
                Description = shoeType.Description
            };
        }
    }
}
