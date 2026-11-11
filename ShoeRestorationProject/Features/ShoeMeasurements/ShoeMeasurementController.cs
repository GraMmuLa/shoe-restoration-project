using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.ShoeMeasurements.DTO;

namespace ShoeRestorationProject.Features.ShoeMeasurements;

[ApiController]
[Route("api/shoe-measurements")]
public class ShoeMeasurementController(ShoeMeasurementService service, ILogger<ShoeMeasurementController> logger)
    : ControllerBase
{
    /// <summary>Retrieves all shoe-measurement.</summary>
    /// <remarks>Returns every shoe-measurement currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of shoe-measurement, or no content if no shoe-measurement exist.</returns>
    /// <response code="200">Returns the list of shoe-measurement.</response>
    /// <response code="204">No shoe-measurement were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<ShoeMeasurementResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/shoe-measurements - request to get all shoe-measurement");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0)
            {
                logger.LogWarning("GET /api/shoe-measurements - no shoe-measurement found");
                return NoContent();
            }

            logger.LogInformation("GET /api/shoe-measurements - returned {Count} shoe-measurement", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/shoe-measurement");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>Retrieves a shoe-measurement by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShoeMeasurementResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/shoe-measurements/{Id} - request to get shoe-measurement by id", id);
        try
        {
            var result = await service.GetByIdAsync(id);
            logger.LogInformation("GET /api/shoe-measurements/{Id} - shoe-measurement found: {@ShoeMeasurement}", id,
                result);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "GET /api/shoe-measurements/{Id} - shoe-measurement not found", id);
            return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/shoe-measurements/{Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] ShoeMeasurementRequest shoeMeasurement)
    {
        logger.LogInformation("POST /api/shoe-measurements - attempt to add shoe-measurement: {@ShoeMeasurementRequest}",
            shoeMeasurement);
        try
        {
            var result = await service.AddAsync(shoeMeasurement);
            logger.LogInformation("POST /api/shoe-measurements - shoe-measurement added successfully");
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
        }
        catch (UniqueObjectException ex)
        {
            logger.LogWarning(ex, "POST /api/shoe-measurements - shoe-measurement is already exists");
            return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in POST /api/shoe-measurements");
            return BadRequest();
        }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, ShoeMeasurementRequest obj)
    {
        logger.LogInformation(
            "PUT /api/shoe-measurements - attempt to update shoe-measurement: {@ShoeMeasurementRequest}", obj);
        try
        {
            var result = await service.UpdateAsync(id, obj);
            logger.LogInformation("PUT /api/shoe-measurements - shoe-measurement updated successfully");
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "PUT /api/shoe-measurements - shoe-measurement not found");
            return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in PUT /api/shoe-measurements");
            return BadRequest();
        }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/shoe-measurements/{Id} - attempt to delete shoe-measurement", id);
        try
        {
            var result = await service.DeleteAsync(id);
            logger.LogInformation("DELETE /api/shoe-measurements/{Id} - shoe-measurement deleted successfully", id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "DELETE /api/shoe-measurements/{Id} - shoe-measurement not found", id);
            return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in DELETE /api/shoe-measurements/{Id}", id);
            return BadRequest();
        }
    }
}