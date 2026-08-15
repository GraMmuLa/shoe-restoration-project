using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class SkinTypesService : BaseService<SkinTypeDto, SkinType>, ISkinTypesService
    {
        private readonly ISkinTypesRepository _skinTypesRepository;

        public SkinTypesService(ISkinTypesRepository skinTypesRepository, IUnitOfWork unitOfWork)
            : base(skinTypesRepository, unitOfWork)
        {
            _skinTypesRepository = skinTypesRepository;
        }
    }
}
