using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Features.Shoes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoeController(ShoeService service, ILogger<ShoeController> logger) : ControllerBase
    {
        /// <summary>Retrieves all shoe.</summary>
        /// <remarks>Returns every shoe currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of shoe, or no content if no shoe exist.</returns>
        /// <response code="200">Returns the list of shoe.</response>
        /// <response code="204">No shoe were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ShoeResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/shoes - request to get all shoe");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0) { logger.LogWarning("GET /api/shoes - no shoe found"); return NoContent(); }
                logger.LogInformation("GET /api/shoes - returned {Count} shoe", result.Count);
                return Ok(result);
            }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoe"); return StatusCode(500, "Internal server error."); }
        }

        /// <summary>Retrieves a shoe by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShoeResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/shoes/{Id} - request to get shoe by id", id);
            try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/shoes/{Id} - shoe found: {@ Shoe}", id, result); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/shoes/{Id} - shoe not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoes/{Id}", id); return StatusCode(500, "Internal server error."); }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ShoeRequest shoe)
        {
            logger.LogInformation("POST /api/shoes - attempt to add shoe: {@ ShoeRequest}", shoe);
            try { var result = await service.AddAsync(shoe); logger.LogInformation("POST /api/shoes - shoe added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
            catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/shoes - shoe is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in POST /api/shoes"); return BadRequest(); }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
                [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, ShoeRequest obj)
        {
            logger.LogInformation("PUT /api/shoes - attempt to update shoe: {@ ShoeRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/shoes - shoe updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/shoes - shoe not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/shoes");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/shoes/{Id} - attempt to delete shoe", id);
            try { ShoeResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/shoes/{Id} - shoe deleted successfully", id); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/shoes/{Id} - shoe not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
            catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/shoes/{Id}", id); return BadRequest(); }
        }
    }
}
