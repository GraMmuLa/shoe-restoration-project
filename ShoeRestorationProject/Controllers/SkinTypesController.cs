using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkinTypesController(SkinTypeService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all skin types.
        /// </summary>
        /// <remarks>Returns every skin type currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of skin types, or no content if none exist.</returns>
        /// <response code="200">Returns the list of skin types.</response>
        /// <response code="204">No skin types were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SkinTypeDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a skin type by its identifier.
        /// </summary>
        /// <remarks>Looks up the skin type using the identifier in the route. A skin type that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the skin type to retrieve.</param>
        /// <returns>The requested skin type when found.</returns>
        /// <response code="200">Returns the requested skin type.</response>
        /// <response code="404">No skin type exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SkinTypeDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a skin type using the submitted form data.
        /// </summary>
        /// <remarks>Submit the skin type fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The skin type data to create.</param>
        /// <returns>An empty success response when the skin type is created.</returns>
        /// <response code="200">The skin type was created successfully.</response>
        /// <response code="400">The skin type could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SkinTypeDto obj)
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
        /// Updates an existing skin type using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated skin type object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated skin type data, including its identifier.</param>
        /// <returns>An empty success response when the skin type is updated.</returns>
        /// <response code="200">The skin type was updated successfully.</response>
        /// <response code="400">The skin type could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(SkinTypeDto obj)
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
        /// Deletes a skin type by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the skin type exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the skin type to delete.</param>
        /// <returns>An empty success response when the skin type is deleted.</returns>
        /// <response code="200">The skin type was deleted successfully.</response>
        /// <response code="404">No skin type exists with the specified identifier.</response>
        /// <response code="400">The skin type could not be deleted.</response>
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
