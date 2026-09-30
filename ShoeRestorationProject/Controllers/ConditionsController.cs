using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConditionsController(ConditionService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all conditions.
        /// </summary>
        /// <remarks>Returns every condition currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of conditions, or no content if no conditions exist.</returns>
        /// <response code="200">Returns the list of conditions.</response>
        /// <response code="204">No conditions were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ConditionDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a condition by its identifier.
        /// </summary>
        /// <remarks>Looks up the condition using the identifier in the route. A condition that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the condition to retrieve.</param>
        /// <returns>The requested condition when found.</returns>
        /// <response code="200">Returns the requested condition.</response>
        /// <response code="404">No condition exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ConditionDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a condition using the submitted form data.
        /// </summary>
        /// <remarks>Submit the condition fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The condition data to create.</param>
        /// <returns>An empty success response when the condition is created.</returns>
        /// <response code="200">The condition was created successfully.</response>
        /// <response code="400">The condition could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ConditionDto obj)
        {
            try
            {
                await service.AddAsync(obj);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates an existing condition using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated condition object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated condition data, including the identifier of the condition to update.</param>
        /// <returns>An empty success response when the condition is updated.</returns>
        /// <response code="200">The condition was updated successfully.</response>
        /// <response code="400">The condition could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(ConditionDto obj)
        {
            try
            {
                service.Update(obj);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a condition by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the condition exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the condition to delete.</param>
        /// <returns>An empty success response when the condition is deleted.</returns>
        /// <response code="200">The condition was deleted successfully.</response>
        /// <response code="404">No condition exists with the specified identifier.</response>
        /// <response code="400">The condition could not be deleted.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var entity = await service.GetByIdAsync(id);
                if (entity is null)
                    return NotFound();

                service.Delete(entity);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
