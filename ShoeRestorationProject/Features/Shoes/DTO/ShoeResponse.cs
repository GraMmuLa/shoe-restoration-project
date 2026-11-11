namespace ShoeRestorationProject.Features.Shoes.DTO;

using ShoeRestorationProject.Features.ShoeTypes.DTO;
using ShoeRestorationProject.Features.Conditions.DTO;
using ShoeRestorationProject.Features.Brands.DTO;
using ShoeRestorationProject.Features.SkinTypes.DTO;
using ShoeRestorationProject.Features.Sizes.DTO;
using ShoeRestorationProject.Features.Colors.DTO;

public record ShoeResponse(int Id, string Title,
    string Description, ConditionResponse Condition,
    BrandResponse Brand, SkinTypeResponse SkinType,
    SizeResponse Size, ShoeTypeResponse ShoeType,
    ColorResponse Color);