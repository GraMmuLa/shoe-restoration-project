namespace ShoeRestorationProject.Features.Shoes.DTO;

public record ShoeRequest(string Title, string Description,
    int ConditionId, int BrandId,
    int SkinTypeId, int SizeId,
    int ShoeTypeId, int ColorId);