namespace ShoeRestorationProject.Features.ShoeTypes;
public record ShoeTypeRequest(string Name, string? Description);
public record ShoeTypeResponse(int Id, string Name, string? Description);
