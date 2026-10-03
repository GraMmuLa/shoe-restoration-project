using ShoeRestorationProject.Features.Countries;

namespace ShoeRestorationProject.Features.Brands;
public record BrandRequest(string Name, int CountryId);
public record BrandResponse(int Id, string Name, CountryResponse Country);
