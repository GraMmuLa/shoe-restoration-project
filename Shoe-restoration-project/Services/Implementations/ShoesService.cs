using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class ShoesService : BaseService<ShoeDto, Shoe>, IShoesService
    {
        private readonly IShoesRepository _shoesRepository;

        public ShoesService(IShoesRepository shoesRepository, IUnitOfWork unitOfWork)
            : base(shoesRepository, unitOfWork)
        {
            _shoesRepository = shoesRepository;
        }
    }
}
