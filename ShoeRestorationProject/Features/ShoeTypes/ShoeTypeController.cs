using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.ShoeTypes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoeTypeController(ShoeTypeService service, ILogger<ShoeTypeController> logger) : ControllerBase
    {
        /// <summary>Retrieves all shoetype.</summary>
        /// <remarks>Returns every shoetype currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of shoetype, or no content if no shoetype exist.</returns>
        /// <response code="200">Returns the list of shoetype.</response>
        /// <response code="204">No shoetype were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ShoeTypeResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/shoetypes - request to get all shoetype");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0) { logger.LogWarning("GET /api/shoetypes - no shoetype found"); return NoContent(); }
                logger.LogInformation("GET /api/shoetypes - returned {Count} shoetype", result.Count);
                return Ok(result);
            }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoetype"); return StatusCode(500, "Internal server error."); }
        }

        /// <summary>Retrieves a shoetype by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShoeTypeResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/shoetypes/{Id} - request to get shoetype by id", id);
            try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/shoetypes/{Id} - shoetype found: {@ Shoetype}", id, result); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/shoetypes/{Id} - shoetype not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/shoetypes/{Id}", id); return StatusCode(500, "Internal server error."); }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ShoeTypeRequest shoetype)
        {
            logger.LogInformation("POST /api/shoetypes - attempt to add shoetype: {@ ShoetypeRequest}", shoetype);
            try { var result = await service.AddAsync(shoetype); logger.LogInformation("POST /api/shoetypes - shoetype added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
            catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/shoetypes - shoetype is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in POST /api/shoetypes"); return BadRequest(); }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
                [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, ShoeTypeRequest obj)
        {
            logger.LogInformation("PUT /api/shoetypes - attempt to update shoetype: {@ ShoeTypeRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/shoetypes - shoetype updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/shoetypes - shoetype not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/shoetypes");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/shoetypes/{Id} - attempt to delete shoetype", id);
            try { ShoeTypeResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/shoetypes/{Id} - shoetype deleted successfully", id); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/shoetypes/{Id} - shoetype not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
            catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/shoetypes/{Id}", id); return BadRequest(); }
        }
    }
}
