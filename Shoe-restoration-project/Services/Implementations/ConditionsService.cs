using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class ConditionsService : BaseService<ConditionDto, Condition>, IConditionsService
    {
        private readonly IConditionsRepository _conditionsRepository;

        public ConditionsService(IConditionsRepository conditionsRepository, IUnitOfWork unitOfWork) : base(conditionsRepository, unitOfWork)
        {
            _conditionsRepository = conditionsRepository;
        }
    }
}
