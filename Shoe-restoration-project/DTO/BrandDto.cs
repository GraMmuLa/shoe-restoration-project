using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class BrandDto : IDtoObject<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public BrandDto()
        {
        }

        public Brand GetModel()
        {
            return new Brand()
            {
                Id = this.Id,
                CountryId = this.CountryId,
                Name = this.Name
            };
        }
        public static IDtoObject<Brand> FromModel(Brand brand)
        {
            return new BrandDto()
            {
                Id = brand.Id,
                Name = brand.Name,
                CountryId = brand.CountryId
            };
        }
    }
}
