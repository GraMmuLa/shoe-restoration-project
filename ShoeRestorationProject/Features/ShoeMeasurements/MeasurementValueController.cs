using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.ShoeMeasurements;

[ApiController]
[Route("api/[controller]")]
public class MeasurementValueController(ShoeMeasurementService service, ILogger<MeasurementValueController> logger) : ControllerBase
{
    /// <summary>Retrieves all shoemeasurement.</summary>
    /// <remarks>Returns every shoemeasurement currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of shoemeasurement, or no content if no shoemeasurement exist.</returns>
    /// <response code="200">Returns the list of shoemeasurement.</response>
    /// <response code="204">No shoemeasurement were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<ShoeMeasurementResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/shoemeasurements - request to get all shoemeasurement");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/shoemeasurements - no shoemeasurement found"); return NoContent(); }
            logger.LogInformation("GET /api/shoemeasurements - returned {Count} shoemeasurement", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoemeasurement"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a shoemeasurement by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShoeMeasurementResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/shoemeasurements/{Id} - request to get shoemeasurement by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/shoemeasurements/{Id} - shoemeasurement found: {@ShoeMeasurement}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/shoemeasurements/{Id} - shoemeasurement not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoemeasurements/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] ShoeMeasurementRequest shoeMeasurement)
    {
        logger.LogInformation("POST /api/shoemeasurements - attempt to add shoemeasurement: {@ShoeMeasurementRequest}", shoeMeasurement);
        try { var result = await service.AddAsync(shoeMeasurement); logger.LogInformation("POST /api/shoemeasurements - shoemeasurement added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/shoemeasurements - shoemeasurement is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/shoemeasurements"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, ShoeMeasurementRequest obj)
    {
        logger.LogInformation("PUT /api/shoemeasurements - attempt to update shoemeasurement: {@ShoeMeasurementRequest}", obj);
        try { var result = await service.UpdateAsync(id, obj); logger.LogInformation("PUT /api/shoemeasurements - shoemeasurement updated successfully"); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "PUT /api/shoemeasurements - shoemeasurement not found"); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in PUT /api/shoemeasurements"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/shoemeasurements/{Id} - attempt to delete shoemeasurement", id);
        try { ShoeMeasurementResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/shoemeasurements/{Id} - shoemeasurement deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/shoemeasurements/{Id} - shoemeasurement not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/shoemeasurements/{Id}", id); return BadRequest(); }
    }
}
