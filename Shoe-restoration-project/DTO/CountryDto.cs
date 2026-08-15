using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class CountryDto : IDtoObject<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string IsoCode { get; set; } = null!;

        public Country GetModel()
        {
            return new Country()
            {
                Id = this.Id,
                Name = this.Name,
                IsoCode = this.IsoCode,
            };
        }

        public static IDtoObject<Country> FromModel(Country country)
        {
            return new CountryDto()
            {
                Id = country.Id,
                Name = country.Name,
                IsoCode = country.IsoCode,
            };
        }
    }
}
