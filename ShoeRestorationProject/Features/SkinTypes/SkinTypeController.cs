using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.SkinTypes.DTO;

namespace ShoeRestorationProject.Features.SkinTypes
{
    [ApiController]
    [Route("api/skin-types")]
    public class SkinTypeController(SkinTypeService service, ILogger<SkinTypeController> logger) : ControllerBase
    {
        /// <summary>Retrieves all skin-type.</summary>
        /// <remarks>Returns every skin-type currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of skin-type, or no content if no skin-type exist.</returns>
        /// <response code="200">Returns the list of skin-type.</response>
        /// <response code="204">No skin-type were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<SkinTypeResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/skin-types - request to get all skin-type");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0)
                {
                    logger.LogWarning("GET /api/skin-types - no skin-type found");
                    return NoContent();
                }

                logger.LogInformation("GET /api/skin-types - returned {Count} skin-type", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/skin-type");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>Retrieves a skin-type by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SkinTypeResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/skin-types/{Id} - request to get skin-type by id", id);
            try
            {
                var result = await service.GetByIdAsync(id);
                logger.LogInformation("GET /api/skin-types/{Id} - skin-type found: {@SkinType}", id, result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "GET /api/skin-types/{Id} - skin-type not found", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/skin-types/{Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] SkinTypeRequest skinType)
        {
            logger.LogInformation("POST /api/skin-types - attempt to add skin-type: {@SkinTypeRequest}", skinType);
            try
            {
                var result = await service.AddAsync(skinType);
                logger.LogInformation("POST /api/skin-types - skin-type added successfully");
                return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
            }
            catch (UniqueObjectException ex)
            {
                logger.LogWarning(ex, "POST /api/skin-types - skin-type is already exists");
                return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in POST /api/skin-types");
                return BadRequest();
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, SkinTypeRequest obj)
        {
            logger.LogInformation("PUT /api/skin-types - attempt to update skin-type: {@SkinTypeRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/skin-types - skin-type updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/skin-types - skin-type not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/skin-types");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/skin-types/{Id} - attempt to delete skin-type", id);
            try
            {
                var result = await service.DeleteAsync(id);
                logger.LogInformation("DELETE /api/skin-types/{Id} - skin-type deleted successfully", id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "DELETE /api/skin-types/{Id} - skin-type not found", id);
                return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DELETE /api/skin-types/{Id}", id);
                return BadRequest();
            }
        }
    }
}