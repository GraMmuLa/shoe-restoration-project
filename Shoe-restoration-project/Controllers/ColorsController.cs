using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class ColorsController : BaseApiController<ColorDto, Color>
    {
        private readonly IColorsService _colorsService;
        public ColorsController(IColorsService colorsService) : base(colorsService)
        {
            _colorsService = colorsService;
        }
    }
}
