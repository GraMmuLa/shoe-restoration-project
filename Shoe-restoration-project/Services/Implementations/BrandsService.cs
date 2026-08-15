using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class BrandsService : BaseService<BrandDto, Brand>, IBrandsService
    {
        private readonly IBrandsRepository _brandsRepository;

        public BrandsService(IBrandsRepository brandsRepository, IUnitOfWork unitOfWork) : base(brandsRepository, unitOfWork)
        {
            _brandsRepository = brandsRepository;
        }
    }
}
