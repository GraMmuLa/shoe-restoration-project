using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class SizesService : BaseService<SizeDto, Size>, ISizesService
    {
        private readonly ISizesRepository _sizesRepository;

        public SizesService(ISizesRepository sizesRepository, IUnitOfWork unitOfWork) : base(sizesRepository, unitOfWork)
        {
            _sizesRepository = sizesRepository;
        }
    }
}
