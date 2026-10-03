using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.ShoeImages;

[ApiController]
[Route("api/[controller]")]
public class ShoeImageController(ShoeImageService service, ILogger<ShoeImageController> logger) : ControllerBase
{
    /// <summary>Retrieves all shoeimage.</summary>
    /// <remarks>Returns every shoeimage currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of shoeimage, or no content if no shoeimage exist.</returns>
    /// <response code="200">Returns the list of shoeimage.</response>
    /// <response code="204">No shoeimage were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<ShoeImageResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/shoeimages - request to get all shoeimage");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/shoeimages - no shoeimage found"); return NoContent(); }
            logger.LogInformation("GET /api/shoeimages - returned {Count} shoeimage", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoeimage"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a shoeimage by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ShoeImageResponse>> GetByIdAsync(Guid id)
    {
        logger.LogInformation("GET /api/shoeimages/{Id} - request to get shoeimage by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/shoeimages/{Id} - shoeimage found: {@Shoeimage}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/shoeimages/{Id} - shoeimage not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoeimages/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] ShoeImageRequest shoeimage)
    {
        logger.LogInformation("POST /api/shoeimages - attempt to add shoeimage: {@ShoeImageRequest}", shoeimage);
        try { var result = await service.AddAsync(shoeimage); logger.LogInformation("POST /api/shoeimages - shoeimage added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/shoeimages - shoeimage is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/shoeimages"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
            [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ShoeImageRequest obj)
        {
            logger.LogInformation("PUT /api/shoeimages - attempt to update shoeimage: {@ ShoeImageRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/shoeimages - shoeimage updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/shoeimages - shoeimage not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/shoeimages");
                return BadRequest();
            }
        }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        logger.LogInformation("DELETE /api/shoeimages/{Id} - attempt to delete shoeimage", id);
        try { ShoeImageResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/shoeimages/{Id} - shoeimage deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/shoeimages/{Id} - shoeimage not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/shoeimages/{Id}", id); return BadRequest(); }
    }
}
