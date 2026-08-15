using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class ShoesController : BaseApiController<ShoeDto, Shoe>
    {
        private readonly IShoesService _shoesService;

        public ShoesController(IShoesService shoesService) : base(shoesService)
        {
            _shoesService = shoesService;
        }
    }
}
