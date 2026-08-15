using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController<TDto, TModel> : ControllerBase
        where TDto : class, IDtoObject<TModel>
        where TModel : class
    {
        private readonly IBaseService<TDto, TModel> _service;

        public BaseApiController(IBaseService<TDto, TModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual ActionResult<IList<TDto>> GetAll()
        {
            IList<TDto> result = _service.GetAll();

            return result.Count > 0 ? Ok(result) : NoContent();
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TDto> GetById(object id)
        {
            TDto? result = _service.GetById(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public virtual ActionResult Add([FromForm] TDto obj)
        {
            try
            {
                _service.Add(obj);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public virtual ActionResult Update(TDto obj)
        {
            try
            {
                _service.Update(obj);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(object id)
        {
            try
            {
                TDto? entity = _service.GetById(id);
                if (entity is null)
                    return NotFound();
                _service.Delete(entity);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
