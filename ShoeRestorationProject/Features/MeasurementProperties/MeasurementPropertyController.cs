using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.MeasurementProperties.DTO;

namespace ShoeRestorationProject.Features.MeasurementProperties;

[ApiController]
[Route("api/measurement-properties")]
public class MeasurementPropertyController(
    MeasurementPropertyService service,
    ILogger<MeasurementPropertyController> logger) : ControllerBase
{
    /// <summary>Retrieves all measurement property.</summary>
    /// <remarks>Returns every measurement property currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of measurement property, or no content if no measurement property exist.</returns>
    /// <response code="200">Returns the list of measurement property.</response>
    /// <response code="204">No measurement property were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<MeasurementPropertyResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/measurement-properties - request to get all measurement property");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0)
            {
                logger.LogWarning("GET /api/measurement-properties - no measurement property found");
                return NoContent();
            }

            logger.LogInformation("GET /api/measurement-properties - returned {Count} measurement property",
                result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/measurement property");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>Retrieves a measurement property by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MeasurementPropertyResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/measurement-properties/{Id} - request to get measurement property by id", id);
        try
        {
            var result = await service.GetByIdAsync(id);
            logger.LogInformation(
                "GET /api/measurement-properties/{Id} - measurement property found: {@MeasurementProperty}", id, result);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "GET /api/measurement-properties/{Id} - measurement property not found", id);
            return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/measurement-properties/{Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] MeasurementPropertyRequest measurementProperty)
    {
        logger.LogInformation(
            "POST /api/measurement-properties - attempt to add measurement property: {@MeasurementPropertyRequest}",
            measurementProperty);
        try
        {
            var result = await service.AddAsync(measurementProperty);
            logger.LogInformation("POST /api/measurement-properties - measurement property added successfully");
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
        }
        catch (UniqueObjectException ex)
        {
            logger.LogWarning(ex, "POST /api/measurement-properties - measurement property is already exists");
            return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in POST /api/measurement-properties");
            return BadRequest();
        }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, MeasurementPropertyRequest obj)
    {
        logger.LogInformation(
            "PUT /api/measurement-properties - attempt to update measurement property: {@MeasurementPropertyRequest}",
            obj);
        try
        {
            var result = await service.UpdateAsync(id, obj);
            logger.LogInformation("PUT /api/measurement-properties - measurement property updated successfully");
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "PUT /api/measurement-properties - measurement property not found");
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in PUT /api/measurement-properties");
            return BadRequest();
        }
    }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/measurement-properties/{Id} - attempt to delete measurement property", id);
        try
        {
            MeasurementPropertyResponse result = await service.DeleteAsync(id);
            logger.LogInformation("DELETE /api/measurement-properties/{Id} - measurement property deleted successfully",
                id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "DELETE /api/measurement-properties/{Id} - measurement property not found", id);
            return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in DELETE /api/measurement-properties/{Id}", id);
            return BadRequest();
        }
    }
}