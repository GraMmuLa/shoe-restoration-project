using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class ConditionsController : BaseApiController<ConditionDto, Condition>
    {
        private readonly IConditionsService _conditionsService;
        public ConditionsController(IConditionsService conditionsService) : base(conditionsService)
        {
            _conditionsService = conditionsService;
        }
    }
}
