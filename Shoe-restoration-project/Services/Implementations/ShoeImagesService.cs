using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class ShoeImagesService : BaseService<ShoeImageDto, ShoeImage>, IShoeImagesService
    {
        public readonly IShoeImagesRepository _shoeImagesRepository;

        public ShoeImagesService(IShoeImagesRepository shoeImagesRepository, IUnitOfWork unitOfWork)
            : base(shoeImagesRepository, unitOfWork)
        {
            _shoeImagesRepository = shoeImagesRepository;
        }
    }
}
