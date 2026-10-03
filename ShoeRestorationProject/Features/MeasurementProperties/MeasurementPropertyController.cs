using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Features.MeasurementProperties;

[ApiController]
[Route("api/[controller]")]
public class MeasurementPropertyController(MeasurementPropertyService service, ILogger<MeasurementPropertyController> logger) : ControllerBase
{
    /// <summary>Retrieves all measurementproperty.</summary>
    /// <remarks>Returns every measurementproperty currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of measurementproperty, or no content if no measurementproperty exist.</returns>
    /// <response code="200">Returns the list of measurementproperty.</response>
    /// <response code="204">No measurementproperty were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<MeasurementPropertyResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/measurementproperties - request to get all measurementproperty");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/measurementproperties - no measurementproperty found"); return NoContent(); }
            logger.LogInformation("GET /api/measurementproperties - returned {Count} measurementproperty", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/measurementproperty"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a measurementproperty by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MeasurementPropertyResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/measurementproperties/{Id} - request to get measurementproperty by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/measurementproperties/{Id} - measurementproperty found: {@MeasurementProperty}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/measurementproperties/{Id} - measurementproperty not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/measurementproperties/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] MeasurementPropertyRequest measurementproperty)
    {
        logger.LogInformation("POST /api/measurementproperties - attempt to add measurementproperty: {@MeasurementPropertyRequest}", measurementproperty);
        try { var result = await service.AddAsync(measurementproperty); logger.LogInformation("POST /api/measurementproperties - measurementproperty added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/measurementproperties - measurementproperty is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/measurementproperties"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
            [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, MeasurementPropertyRequest obj)
        {
            logger.LogInformation("PUT /api/measurementpropertys - attempt to update measurementproperty: {@ MeasurementPropertyRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/measurementpropertys - measurementproperty updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/measurementpropertys - measurementproperty not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/measurementpropertys");
                return BadRequest();
            }
        }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/measurementproperties/{Id} - attempt to delete measurementproperty", id);
        try { MeasurementPropertyResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/measurementproperties/{Id} - measurementproperty deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/measurementproperties/{Id} - measurementproperty not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/measurementproperties/{Id}", id); return BadRequest(); }
    }
}