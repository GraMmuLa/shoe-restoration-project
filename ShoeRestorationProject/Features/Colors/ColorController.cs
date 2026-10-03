using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.Colors;

[ApiController]
[Route("api/[controller]")]
public class ColorController(ColorService service, ILogger<ColorController> logger) : ControllerBase
{
    /// <summary>Retrieves all color.</summary>
    /// <remarks>Returns every color currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of color, or no content if no color exist.</returns>
    /// <response code="200">Returns the list of color.</response>
    /// <response code="204">No color were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<ColorResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/colors - request to get all color");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/colors - no color found"); return NoContent(); }
            logger.LogInformation("GET /api/colors - returned {Count} color", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/color"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a color by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ColorResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/colors/{Id} - request to get color by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/colors/{Id} - color found: {@Color}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/colors/{Id} - color not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/colors/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] ColorRequest color)
    {
        logger.LogInformation("POST /api/colors - attempt to add color: {@ColorRequest}", color);
        try { var result = await service.AddAsync(color); logger.LogInformation("POST /api/colors - color added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/colors - color is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/colors"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
            [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, ColorRequest obj)
        {
            logger.LogInformation("PUT /api/colors - attempt to update color: {@ColorRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/colors - color updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/colors - color not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/colors");
                return BadRequest();
            }
        }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/colors/{Id} - attempt to delete color", id);
        try { ColorResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/colors/{Id} - color deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/colors/{Id} - color not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/colors/{Id}", id); return BadRequest(); }
    }
}