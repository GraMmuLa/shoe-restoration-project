using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class ColorDto : IDtoObject<Color>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Color GetModel()
        {
            return new Color()
            {
                Id = this.Id,
                Name = this.Name
            };
        }
        public static IDtoObject<Color> FromModel(Color color)
        {
            return new ColorDto()
            {
                Id = color.Id,
                Name = color.Name,
            };
        }
    }
}
