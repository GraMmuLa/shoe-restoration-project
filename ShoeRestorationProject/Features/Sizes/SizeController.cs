using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.Sizes.DTO;

namespace ShoeRestorationProject.Features.Sizes
{
    [ApiController]
    [Route("api/sizes")]
    public class SizeController(SizeService service, ILogger<SizeController> logger) : ControllerBase
    {
        /// <summary>Retrieves all sizes.</summary>
        /// <remarks>Returns every size currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of sizes, or no content if no sizes exist.</returns>
        /// <response code="200">Returns the list of sizes.</response>
        /// <response code="204">No sizes were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SizeResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/sizes - request to get all sizes");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0)
                {
                    logger.LogWarning("GET /api/sizes - no sizes found");
                    return NoContent();
                }

                logger.LogInformation("GET /api/sizes - returned {Count} sizes", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/sizes");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>Retrieves a sizes by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SizeResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/sizes/{Id} - request to get size by id", id);
            try
            {
                var result = await service.GetByIdAsync(id);
                logger.LogInformation("GET /api/sizes/{Id} - size found: {@Size}", id, result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "GET /api/sizes/{Id} - size not found", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/sizes/{Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SizeRequest sizes)
        {
            logger.LogInformation("POST /api/sizes - attempt to add size: {@SizeRequest}", sizes);
            try
            {
                var result = await service.AddAsync(sizes);
                logger.LogInformation("POST /api/sizes - size added successfully");
                return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
            }
            catch (UniqueObjectException ex)
            {
                logger.LogWarning(ex, "POST /api/sizes - size is already exists");
                return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in POST /api/sizes");
                return BadRequest();
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, SizeRequest obj)
        {
            logger.LogInformation("PUT /api/sizes - attempt to update size: {@SizeRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/sizes - size updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/sizes - size not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/sizes");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/sizes/{Id} - attempt to delete size", id);
            try
            {
                var result = await service.DeleteAsync(id);
                logger.LogInformation("DELETE /api/sizes/{Id} - size deleted successfully", id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "DELETE /api/sizes/{Id} - size not found", id);
                return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DELETE /api/sizes/{Id}", id);
                return BadRequest();
            }
        }
    }
}