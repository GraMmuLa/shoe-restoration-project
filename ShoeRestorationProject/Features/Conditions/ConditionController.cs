using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.Conditions;

[ApiController]
[Route("api/[controller]")]
public class ConditionController(ConditionService service, ILogger<ConditionController> logger) : ControllerBase
{
    /// <summary>Retrieves all condition.</summary>
    /// <remarks>Returns every condition currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of condition, or no content if no condition exist.</returns>
    /// <response code="200">Returns the list of condition.</response>
    /// <response code="204">No condition were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<ConditionResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/conditions - request to get all condition");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/conditions - no condition found"); return NoContent(); }
            logger.LogInformation("GET /api/conditions - returned {Count} condition", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/condition"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a condition by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ConditionResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/conditions/{Id} - request to get condition by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/conditions/{Id} - condition found: {@Condition}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/conditions/{Id} - condition not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/conditions/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] ConditionRequest condition)
    {
        logger.LogInformation("POST /api/conditions - attempt to add condition: {@ConditionRequest}", condition);
        try { var result = await service.AddAsync(condition); logger.LogInformation("POST /api/conditions - condition added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/conditions - condition is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/conditions"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
            [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, ConditionRequest obj)
        {
            logger.LogInformation("PUT /api/conditions - attempt to update condition: {@ ConditionRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/conditions - condition updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/conditions - condition not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/conditions");
                return BadRequest();
            }
        }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/conditions/{Id} - attempt to delete condition", id);
        try { ConditionResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/conditions/{Id} - condition deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/conditions/{Id} - condition not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/conditions/{Id}", id); return BadRequest(); }
    }
}