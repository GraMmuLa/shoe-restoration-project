using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.DTO
{
    public record ShoeDto(int Id, string Title, string Description,
        int ConditionId, int BrandId, int SkinTypeId, int SizeId, int ShoeTypeId);
}
