using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoesController(ShoeService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all shoes.
        /// </summary>
        /// <remarks>Returns every shoe currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of shoes, or no content if no shoes exist.</returns>
        /// <response code="200">Returns the list of shoes.</response>
        /// <response code="204">No shoes were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ShoeDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a shoe by its identifier.
        /// </summary>
        /// <remarks>Looks up the shoe using the identifier in the route. A shoe that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the shoe to retrieve.</param>
        /// <returns>The requested shoe when found.</returns>
        /// <response code="200">Returns the requested shoe.</response>
        /// <response code="404">No shoe exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoeDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a shoe using the submitted form data.
        /// </summary>
        /// <remarks>Submit the shoe fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The shoe data to create.</param>
        /// <returns>An empty success response when the shoe is created.</returns>
        /// <response code="200">The shoe was created successfully.</response>
        /// <response code="400">The shoe could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ShoeDto obj)
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
        /// Updates an existing shoe using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated shoe object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated shoe data, including the identifier of the shoe to update.</param>
        /// <returns>An empty success response when the shoe is updated.</returns>
        /// <response code="200">The shoe was updated successfully.</response>
        /// <response code="400">The shoe could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(ShoeDto obj)
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
        /// Deletes a shoe by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the shoe exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the shoe to delete.</param>
        /// <returns>An empty success response when the shoe is deleted.</returns>
        /// <response code="200">The shoe was deleted successfully.</response>
        /// <response code="404">No shoe exists with the specified identifier.</response>
        /// <response code="400">The shoe could not be deleted.</response>
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
