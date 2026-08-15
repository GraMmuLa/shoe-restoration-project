using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class SkinTypesController : BaseApiController<SkinTypeDto, SkinType>
    {
        private readonly ISkinTypesService _skinTypesService;

        public SkinTypesController(ISkinTypesService skinTypesService) : base(skinTypesService)
        {
            _skinTypesService = skinTypesService;
        }
    }
}
