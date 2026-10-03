using ShoeRestorationProject.Features.Shoes;

namespace ShoeRestorationProject.Features.ShoeImages;
public record ShoeImageRequest(string Name, int ShoeId);
public record ShoeImageResponse(Guid Id, string Name, ShoeResponse ShoeId);
