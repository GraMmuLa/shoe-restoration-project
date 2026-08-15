using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class ColorsService : BaseService<ColorDto, Color>, IColorsService
    {
        private readonly IColorsRepository _colorsRepository;

        public ColorsService(IColorsRepository colorsRepository, IUnitOfWork unitOfWork) : base(colorsRepository, unitOfWork)
        {
            _colorsRepository = colorsRepository;
        }
    }
}
