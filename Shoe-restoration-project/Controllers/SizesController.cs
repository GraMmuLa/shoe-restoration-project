using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class SizesController : BaseApiController<SizeDto, Size>
    {
        private readonly ISizesService _sizesService;

        public SizesController(ISizesService sizesService) : base(sizesService)
        {
            _sizesService = sizesService;
        }
    }
}
