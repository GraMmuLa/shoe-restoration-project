using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementMetricsController(MeasurementMetricService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all measurement metrics.
        /// </summary>
        /// <remarks>Returns every measurement metric currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of measurement metrics, or no content if none exist.</returns>
        /// <response code="200">Returns the list of measurement metrics.</response>
        /// <response code="204">No measurement metrics were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<MeasurementMetricDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a measurement metric by its identifier.
        /// </summary>
        /// <remarks>Looks up the measurement metric using the identifier in the route. A metric that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the measurement metric to retrieve.</param>
        /// <returns>The requested measurement metric when found.</returns>
        /// <response code="200">Returns the requested measurement metric.</response>
        /// <response code="404">No measurement metric exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementMetricDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a measurement metric using the submitted form data.
        /// </summary>
        /// <remarks>Submit the measurement metric fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The measurement metric data to create.</param>
        /// <returns>An empty success response when the measurement metric is created.</returns>
        /// <response code="200">The measurement metric was created successfully.</response>
        /// <response code="400">The measurement metric could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] MeasurementMetricDto obj)
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
        /// Updates an existing measurement metric using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated measurement metric object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated measurement metric data, including its identifier.</param>
        /// <returns>An empty success response when the measurement metric is updated.</returns>
        /// <response code="200">The measurement metric was updated successfully.</response>
        /// <response code="400">The measurement metric could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(MeasurementMetricDto obj)
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
        /// Deletes a measurement metric by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the measurement metric exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the measurement metric to delete.</param>
        /// <returns>An empty success response when the measurement metric is deleted.</returns>
        /// <response code="200">The measurement metric was deleted successfully.</response>
        /// <response code="404">No measurement metric exists with the specified identifier.</response>
        /// <response code="400">The measurement metric could not be deleted.</response>
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
