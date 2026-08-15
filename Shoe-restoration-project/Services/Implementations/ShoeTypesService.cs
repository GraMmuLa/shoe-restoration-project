using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class ShoeTypesService : BaseService<ShoeTypeDto, ShoeType>, IShoeTypesService
    {
        private readonly IShoeTypesRepository _shoeTypesRepository;

        public ShoeTypesService(IShoeTypesRepository shoeTypesRepository, IUnitOfWork unitOfWork)
            : base(shoeTypesRepository, unitOfWork)
        {
            _shoeTypesRepository = shoeTypesRepository;
        }
    }
}
