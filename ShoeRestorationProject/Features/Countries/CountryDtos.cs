namespace ShoeRestorationProject.Features.Countries;
public record CountryRequest(string Name, string IsoCode);
public record CountryResponse(int Id, string Name, string IsoCode);
