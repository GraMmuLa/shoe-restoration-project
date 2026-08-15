using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class BrandsController : BaseApiController<BrandDto, Brand>
    {
        private readonly IBrandsService _brandsService;
        public BrandsController(IBrandsService brandsService) : base(brandsService)
        {
            _brandsService = brandsService;
        }
    }
}
