using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.ShoeImages.DTO;

namespace ShoeRestorationProject.Features.ShoeImages;

[ApiController]
[Route("api/shoe-images")]
public class ShoeImageController(ShoeImageService service, ILogger<ShoeImageController> logger) : ControllerBase
{
    /// <summary>Retrieves all shoe-image.</summary>
    /// <remarks>Returns every shoe-image currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of shoe-image, or no content if no shoe-image exist.</returns>
    /// <response code="200">Returns the list of shoe-image.</response>
    /// <response code="204">No shoe-image were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<ShoeImageResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/shoe-images - request to get all shoe-image");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0)
            {
                logger.LogWarning("GET /api/shoe-images - no shoe-image found");
                return NoContent();
            }

            logger.LogInformation("GET /api/shoe-images - returned {Count} shoe-image", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/shoe-image");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>Retrieves a shoe-image by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ShoeImageResponse>> GetByIdAsync(Guid id)
    {
        logger.LogInformation("GET /api/shoe-images/{Id} - request to get shoe-image by id", id);
        try
        {
            var result = await service.GetByIdAsync(id);
            logger.LogInformation("GET /api/shoe-images/{Id} - shoe-image found: {@Shoeimage}", id, result);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "GET /api/shoe-images/{Id} - shoe-image not found", id);
            return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GET /api/shoe-images/{Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] ShoeImageRequest shoeImage)
    {
        logger.LogInformation("POST /api/shoe-images - attempt to add shoe-image: {@ShoeImageRequest}", shoeImage);
        try
        {
            var result = await service.AddAsync(shoeImage);
            logger.LogInformation("POST /api/shoe-images - shoe-image added successfully");
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
        }
        catch (UniqueObjectException ex)
        {
            logger.LogWarning(ex, "POST /api/shoe-images - shoe-image is already exists");
            return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in POST /api/shoe-images");
            return BadRequest();
        }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, ShoeImageRequest obj)
    {
        logger.LogInformation("PUT /api/shoe-images - attempt to update shoe-image: {@ShoeImageRequest}", obj);
        try
        {
            var result = await service.UpdateAsync(id, obj);
            logger.LogInformation("PUT /api/shoe-images - shoe-image updated successfully");
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "PUT /api/shoe-images - shoe-image not found");
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in PUT /api/shoe-images");
            return BadRequest();
        }
    }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        logger.LogInformation("DELETE /api/shoe-images/{Id} - attempt to delete shoe-image", id);
        try
        {
            ShoeImageResponse result = await service.DeleteAsync(id);
            logger.LogInformation("DELETE /api/shoe-images/{Id} - shoe-image deleted successfully", id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "DELETE /api/shoe-images/{Id} - shoe-image not found", id);
            return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in DELETE /api/shoe-images/{Id}", id);
            return BadRequest();
        }
    }
}