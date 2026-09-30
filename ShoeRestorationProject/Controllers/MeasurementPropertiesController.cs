using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementPropertiesController(MeasurementPropertyService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all measurement properties.
        /// </summary>
        /// <remarks>Returns every measurement property currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of measurement properties, or no content if none exist.</returns>
        /// <response code="200">Returns the list of measurement properties.</response>
        /// <response code="204">No measurement properties were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<MeasurementPropertyDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a measurement property by its identifier.
        /// </summary>
        /// <remarks>Looks up the measurement property using the identifier in the route. A property that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the measurement property to retrieve.</param>
        /// <returns>The requested measurement property when found.</returns>
        /// <response code="200">Returns the requested measurement property.</response>
        /// <response code="404">No measurement property exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementPropertyDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a measurement property using the submitted form data.
        /// </summary>
        /// <remarks>Submit the measurement property fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The measurement property data to create.</param>
        /// <returns>An empty success response when the measurement property is created.</returns>
        /// <response code="200">The measurement property was created successfully.</response>
        /// <response code="400">The measurement property could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] MeasurementPropertyDto obj)
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
        /// Updates an existing measurement property using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated measurement property object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated measurement property data, including its identifier.</param>
        /// <returns>An empty success response when the measurement property is updated.</returns>
        /// <response code="200">The measurement property was updated successfully.</response>
        /// <response code="400">The measurement property could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(MeasurementPropertyDto obj)
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
        /// Deletes a measurement property by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the measurement property exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the measurement property to delete.</param>
        /// <returns>An empty success response when the measurement property is deleted.</returns>
        /// <response code="200">The measurement property was deleted successfully.</response>
        /// <response code="404">No measurement property exists with the specified identifier.</response>
        /// <response code="400">The measurement property could not be deleted.</response>
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
