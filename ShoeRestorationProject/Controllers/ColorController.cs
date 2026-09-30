using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController(ColorService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all colors.
        /// </summary>
        /// <remarks>Returns every color currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of colors, or no content if no colors exist.</returns>
        /// <response code="200">Returns the list of colors.</response>
        /// <response code="204">No colors were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ColorDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a color by its identifier.
        /// </summary>
        /// <remarks>Looks up the color using the identifier in the route. A color that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the color to retrieve.</param>
        /// <returns>The requested color when found.</returns>
        /// <response code="200">Returns the requested color.</response>
        /// <response code="404">No color exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a color using the submitted form data.
        /// </summary>
        /// <remarks>Submit the color fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The color data to create.</param>
        /// <returns>An empty success response when the color is created.</returns>
        /// <response code="200">The color was created successfully.</response>
        /// <response code="400">The color could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ColorDto obj)
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
        /// Updates an existing color using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated color object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated color data, including the identifier of the color to update.</param>
        /// <returns>An empty success response when the color is updated.</returns>
        /// <response code="200">The color was updated successfully.</response>
        /// <response code="400">The color could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(ColorDto obj)
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
        /// Deletes a color by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the color exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the color to delete.</param>
        /// <returns>An empty success response when the color is deleted.</returns>
        /// <response code="200">The color was deleted successfully.</response>
        /// <response code="404">No color exists with the specified identifier.</response>
        /// <response code="400">The color could not be deleted.</response>
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
