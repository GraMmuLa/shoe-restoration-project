using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class ShoeTypesController : BaseApiController<ShoeTypeDto, ShoeType>
    {
        private readonly IShoeTypesService _shoeTypesService;

        public ShoeTypesController(IShoeTypesService shoeTypesService) : base(shoeTypesService)
        {
            _shoeTypesService = shoeTypesService;
        }
    }
}
