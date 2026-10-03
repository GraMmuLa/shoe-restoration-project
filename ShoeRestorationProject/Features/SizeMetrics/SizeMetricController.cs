using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Features.SizeMetrics
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeMetricController(SizeMetricService service, ILogger<SizeMetricController> logger) : ControllerBase
    {
        /// <summary>Retrieves all sizemetric.</summary>
        /// <remarks>Returns every sizemetric currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of sizemetric, or no content if no sizemetric exist.</returns>
        /// <response code="200">Returns the list of sizemetric.</response>
        /// <response code="204">No sizemetric were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SizeMetricResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/sizemetrics - request to get all sizemetric");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0) { logger.LogWarning("GET /api/sizemetrics - no sizemetric found"); return NoContent(); }
                logger.LogInformation("GET /api/sizemetrics - returned {Count} sizemetric", result.Count);
                return Ok(result);
            }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/sizemetric"); return StatusCode(500, "Internal server error."); }
        }

        /// <summary>Retrieves a sizemetric by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SizeMetricResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/sizemetrics/{Id} - request to get sizemetric by id", id);
            try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/sizemetrics/{Id} - sizemetric found: {@ Sizemetric}", id, result); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/sizemetrics/{Id} - sizemetric not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/sizemetrics/{Id}", id); return StatusCode(500, "Internal server error."); }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SizeMetricRequest sizemetric)
        {
            logger.LogInformation("POST /api/sizemetrics - attempt to add sizemetric: {@ SizemetricRequest}", sizemetric);
            try { var result = await service.AddAsync(sizemetric); logger.LogInformation("POST /api/sizemetrics - sizemetric added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
            catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/sizemetrics - sizemetric is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in POST /api/sizemetrics"); return BadRequest(); }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
                [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, SizeMetricRequest obj)
        {
            logger.LogInformation("PUT /api/sizemetrics - attempt to update sizemetric: {@ SizeMetricRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/sizemetrics - sizemetric updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/sizemetrics - sizemetric not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/sizemetrics");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/sizemetrics/{Id} - attempt to delete sizemetric", id);
            try { SizeMetricResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/sizemetrics/{Id} - sizemetric deleted successfully", id); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/sizemetrics/{Id} - sizemetric not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
            catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/sizemetrics/{Id}", id); return BadRequest(); }
        }
    }
}
