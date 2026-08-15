using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class SkinTypeDto : IDtoObject<SkinType>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public SkinType GetModel()
        {
            return new SkinType()
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public static IDtoObject<SkinType> FromModel(SkinType skinType)
        {
            return new SkinTypeDto()
            {
                Id = skinType.Id,
                Name = skinType.Name
            };
        }
    }
}
