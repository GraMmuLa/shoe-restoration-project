using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class ShoeImagesController : BaseApiController<ShoeImageDto, ShoeImage>
    {
        private readonly IShoeImagesService _shoeImagesService;

        public ShoeImagesController(IShoeImagesService shoeImagesService) : base(shoeImagesService)
        {
            _shoeImagesService = shoeImagesService;
        }
    }
}
