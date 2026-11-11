using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.SizeMetrics.DTO;

namespace ShoeRestorationProject.Features.SizeMetrics
{
    [ApiController]
    [Route("api/size-metrics")]
    public class SizeMetricController(SizeMetricService service, ILogger<SizeMetricController> logger) : ControllerBase
    {
        /// <summary>Retrieves all size-metric.</summary>
        /// <remarks>Returns every size-metric currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of size-metric, or no content if no size-metric exist.</returns>
        /// <response code="200">Returns the list of size-metric.</response>
        /// <response code="204">No size-metric were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SizeMetricResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/size-metrics - request to get all size-metric");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0)
                {
                    logger.LogWarning("GET /api/size-metrics - no size-metric found");
                    return NoContent();
                }

                logger.LogInformation("GET /api/size-metrics - returned {Count} size-metric", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/size-metric");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>Retrieves a size-metric by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SizeMetricResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/size-metrics/{Id} - request to get size-metric by id", id);
            try
            {
                var result = await service.GetByIdAsync(id);
                logger.LogInformation("GET /api/size-metrics/{Id} - size-metric found: {@SizeMetric}", id, result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "GET /api/size-metrics/{Id} - size-metric not found", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/size-metrics/{Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SizeMetricRequest sizeMetric)
        {
            logger.LogInformation("POST /api/size-metrics - attempt to add size-metric: {@SizeMetricRequest}",
                sizeMetric);
            try
            {
                var result = await service.AddAsync(sizeMetric);
                logger.LogInformation("POST /api/size-metrics - size-metric added successfully");
                return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
            }
            catch (UniqueObjectException ex)
            {
                logger.LogWarning(ex, "POST /api/size-metrics - size-metric is already exists");
                return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in POST /api/size-metrics");
                return BadRequest();
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, SizeMetricRequest obj)
        {
            logger.LogInformation("PUT /api/size-metrics - attempt to update size-metric: {@SizeMetricRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/size-metrics - size-metric updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/size-metrics - size-metric not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/size-metrics");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/size-metrics/{Id} - attempt to delete size-metric", id);
            try
            {
                SizeMetricResponse result = await service.DeleteAsync(id);
                logger.LogInformation("DELETE /api/size-metrics/{Id} - size-metric deleted successfully", id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "DELETE /api/size-metrics/{Id} - size-metric not found", id);
                return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DELETE /api/size-metrics/{Id}", id);
                return BadRequest();
            }
        }
    }
}