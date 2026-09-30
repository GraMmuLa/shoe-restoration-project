using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoeImagesController(ShoeImageService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all shoe images.
        /// </summary>
        /// <remarks>Returns every shoe image currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of shoe images, or no content if no images exist.</returns>
        /// <response code="200">Returns the list of shoe images.</response>
        /// <response code="204">No shoe images were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ShoeImageDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a shoe image by its GUID identifier.
        /// </summary>
        /// <remarks>Looks up the shoe image using the GUID identifier in the route. An image that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The GUID identifier of the shoe image to retrieve.</param>
        /// <returns>The requested shoe image when found.</returns>
        /// <response code="200">Returns the requested shoe image.</response>
        /// <response code="404">No shoe image exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoeImageDto>> GetByIdAsync(Guid id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a shoe image using the submitted form data.
        /// </summary>
        /// <remarks>Submit the shoe image fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The shoe image data to create.</param>
        /// <returns>An empty success response when the shoe image is created.</returns>
        /// <response code="200">The shoe image was created successfully.</response>
        /// <response code="400">The shoe image could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ShoeImageDto obj)
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
        /// Updates an existing shoe image using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated shoe image object in the request body, including its GUID identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated shoe image data, including its GUID identifier.</param>
        /// <returns>An empty success response when the shoe image is updated.</returns>
        /// <response code="200">The shoe image was updated successfully.</response>
        /// <response code="400">The shoe image could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(ShoeImageDto obj)
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
        /// Deletes a shoe image by its GUID identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the shoe image exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The GUID identifier of the shoe image to delete.</param>
        /// <returns>An empty success response when the shoe image is deleted.</returns>
        /// <response code="200">The shoe image was deleted successfully.</response>
        /// <response code="404">No shoe image exists with the specified identifier.</response>
        /// <response code="400">The shoe image could not be deleted.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
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
