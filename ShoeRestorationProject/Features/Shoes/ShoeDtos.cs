using ShoeRestorationProject.Features.Brands;
using ShoeRestorationProject.Features.Colors;
using ShoeRestorationProject.Features.Conditions;
using ShoeRestorationProject.Features.ShoeTypes;
using ShoeRestorationProject.Features.Sizes;
using ShoeRestorationProject.Features.SkinTypes;

namespace ShoeRestorationProject.Features.Shoes;
public record ShoeRequest(string Title, string Description,
    int ConditionId, int BrandId,
    int SkinTypeId, int SizeId,
    int ShoeTypeId, int ColorId);
public record ShoeResponse(int Id, string Title,
    string Description, ConditionResponse Condition,
    BrandResponse Brand, SkinTypeResponse SkinType,
    SizeResponse Size, ShoeTypeResponse ShoeType,
    ColorResponse Color);
