using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.MeasurementMetrics;

[ApiController]
[Route("api/[controller]")]
public class MeasurementMetricController(MeasurementMetricService service, ILogger<MeasurementMetricController> logger) : ControllerBase
{
    /// <summary>Retrieves all measurementmetric.</summary>
    /// <remarks>Returns every measurementmetric currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of measurementmetric, or no content if no measurementmetric exist.</returns>
    /// <response code="200">Returns the list of measurementmetric.</response>
    /// <response code="204">No measurementmetric were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<MeasurementMetricResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/measurementmetrics - request to get all measurementmetric");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/measurementmetrics - no measurementmetric found"); return NoContent(); }
            logger.LogInformation("GET /api/measurementmetrics - returned {Count} measurementmetric", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/measurementmetric"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a measurementmetric by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MeasurementMetricResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/measurementmetrics/{Id} - request to get measurementmetric by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/measurementmetrics/{Id} - measurementmetric found: {@ Measurementmetric}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/measurementmetrics/{Id} - measurementmetric not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/measurementmetrics/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] MeasurementMetricRequest measurementmetric)
    {
        logger.LogInformation("POST /api/measurementmetrics - attempt to add measurementmetric: {@ MeasurementmetricRequest}", measurementmetric);
        try { var result = await service.AddAsync(measurementmetric); logger.LogInformation("POST /api/measurementmetrics - measurementmetric added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/measurementmetrics - measurementmetric is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/measurementmetrics"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
            [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, MeasurementMetricRequest obj)
        {
            logger.LogInformation("PUT /api/measurementmetrics - attempt to update measurementmetric: {@ MeasurementMetricRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/measurementmetrics - measurementmetric updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/measurementmetrics - measurementmetric not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/measurementmetrics");
                return BadRequest();
            }
        }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/measurementmetrics/{Id} - attempt to delete measurementmetric", id);
        try { MeasurementMetricResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/measurementmetrics/{Id} - measurementmetric deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/measurementmetrics/{Id} - measurementmetric not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/measurementmetrics/{Id}", id); return BadRequest(); }
    }
}
