using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.SkinTypes
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkinTypeController(SkinTypeService service, ILogger<SkinTypeController> logger) : ControllerBase
    {
        /// <summary>Retrieves all skintype.</summary>
        /// <remarks>Returns every skintype currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of skintype, or no content if no skintype exist.</returns>
        /// <response code="200">Returns the list of skintype.</response>
        /// <response code="204">No skintype were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SkinTypeResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/skintypes - request to get all skintype");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0) { logger.LogWarning("GET /api/skintypes - no skintype found"); return NoContent(); }
                logger.LogInformation("GET /api/skintypes - returned {Count} skintype", result.Count);
                return Ok(result);
            }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/skintype"); return StatusCode(500, "Internal server error."); }
        }

        /// <summary>Retrieves a skintype by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SkinTypeResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/skintypes/{Id} - request to get skintype by id", id);
            try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/skintypes/{Id} - skintype found: {@ Skintype}", id, result); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/skintypes/{Id} - skintype not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in GET /api/skintypes/{Id}", id); return StatusCode(500, "Internal server error."); }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SkinTypeRequest skintype)
        {
            logger.LogInformation("POST /api/skintypes - attempt to add skintype: {@ SkintypeRequest}", skintype);
            try { var result = await service.AddAsync(skintype); logger.LogInformation("POST /api/skintypes - skintype added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
            catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/skintypes - skintype is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
            catch (Exception ex) { logger.LogError(ex, "Error in POST /api/skintypes"); return BadRequest(); }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
                [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, SkinTypeRequest obj)
        {
            logger.LogInformation("PUT /api/skintypes - attempt to update skintype: {@ SkinTypeRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/skintypes - skintype updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/skintypes - skintype not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/skintypes");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/skintypes/{Id} - attempt to delete skintype", id);
            try { SkinTypeResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/skintypes/{Id} - skintype deleted successfully", id); return Ok(result); }
            catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/skintypes/{Id} - skintype not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
            catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/skintypes/{Id}", id); return BadRequest(); }
        }
    }
}
