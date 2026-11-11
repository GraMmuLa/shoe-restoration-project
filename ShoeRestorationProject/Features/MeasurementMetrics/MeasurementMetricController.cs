using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.MeasurementMetrics.DTO;

namespace ShoeRestorationProject.Features.MeasurementMetrics;

[ApiController]
[Route("api/measurement-metrics")]
public class MeasurementMetricController(MeasurementMetricService service, ILogger<MeasurementMetricController> logger)
    : ControllerBase
{
    /// <summary>Retrieves all measurement metrics.</summary>
    /// <remarks>Returns every measurement metric currently available.
    /// If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of measurement metrics, or no content if no measurement metric exist.</returns>
    /// <response code="200">Returns the list of measurement metrics.</response>
    /// <response code="204">No measurement metric were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<MeasurementMetricResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/measurement-metrics - request to get all measurement-metrics");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0)
            {
                logger.LogWarning("GET /api/measurement-metrics - no measurement-metrics found");
                return NoContent();
            }

            logger.LogInformation("GET /api/measurement-metrics - returned {Count} measurement-metrics", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/measurement-metrics");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>Retrieves a measurement-metrics by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MeasurementMetricResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/measurement-metrics/{Id} - request to get measurement-metrics by id", id);
        try
        {
            var result = await service.GetByIdAsync(id);
            logger.LogInformation("GET /api/measurement-metrics/{Id} - measurement-metrics found: {@MeasurementMetric}",
                id, result);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "GET /api/measurement-metrics/{Id} - measurement-metrics not found", id);
            return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/measurement-metrics/{Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] MeasurementMetricRequest measurementMetric)
    {
        logger.LogInformation(
            "POST /api/measurement-metrics - attempt to add measurement-metric: {@MeasurementMetricRequest}",
            measurementMetric);
        try
        {
            var result = await service.AddAsync(measurementMetric);
            logger.LogInformation("POST /api/measurement-metrics - measurement-metric added successfully");
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
        }
        catch (UniqueObjectException ex)
        {
            logger.LogWarning(ex, "POST /api/measurement-metrics - measurement-metric is already exists");
            return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in POST /api/measurement-metrics");
            return BadRequest();
        }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, MeasurementMetricRequest obj)
    {
        logger.LogInformation(
            "PUT /api/measurement-metrics - attempt to update measurement-metric: {@MeasurementMetricRequest}", obj);
        try
        {
            var result = await service.UpdateAsync(id, obj);
            logger.LogInformation("PUT /api/measurement-metrics - measurement-metric updated successfully");
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "PUT /api/measurement-metrics - measurement-metric not found");
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in PUT /api/measurement-metrics");
            return BadRequest();
        }
    }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/measurement-metrics/{Id} - attempt to delete measurement-metrics", id);
        try
        {
            MeasurementMetricResponse result = await service.DeleteAsync(id);
            logger.LogInformation("DELETE /api/measurement-metrics/{Id} - measurement-metrics deleted successfully",
                id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "DELETE /api/measurement-metrics/{Id} - measurement-metrics not found", id);
            return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in DELETE /api/measurement-metrics/{Id}", id);
            return BadRequest();
        }
    }
}