using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizesController(SizeService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all sizes.
        /// </summary>
        /// <remarks>Returns every size currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of sizes, or no content if no sizes exist.</returns>
        /// <response code="200">Returns the list of sizes.</response>
        /// <response code="204">No sizes were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SizeDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a size by its identifier.
        /// </summary>
        /// <remarks>Looks up the size using the identifier in the route. A size that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the size to retrieve.</param>
        /// <returns>The requested size when found.</returns>
        /// <response code="200">Returns the requested size.</response>
        /// <response code="404">No size exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a size using the submitted form data.
        /// </summary>
        /// <remarks>Submit the size fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The size data to create.</param>
        /// <returns>An empty success response when the size is created.</returns>
        /// <response code="200">The size was created successfully.</response>
        /// <response code="400">The size could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SizeDto obj)
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
        /// Updates an existing size using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated size object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated size data, including its identifier.</param>
        /// <returns>An empty success response when the size is updated.</returns>
        /// <response code="200">The size was updated successfully.</response>
        /// <response code="400">The size could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(SizeDto obj)
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
        /// Deletes a size by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the size exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the size to delete.</param>
        /// <returns>An empty success response when the size is deleted.</returns>
        /// <response code="200">The size was deleted successfully.</response>
        /// <response code="404">No size exists with the specified identifier.</response>
        /// <response code="400">The size could not be deleted.</response>
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
