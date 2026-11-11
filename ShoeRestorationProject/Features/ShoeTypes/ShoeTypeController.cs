using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.ShoeTypes.DTO;

namespace ShoeRestorationProject.Features.ShoeTypes
{
    [ApiController]
    [Route("api/shoe-types")]
    public class ShoeTypeController(ShoeTypeService service, ILogger<ShoeTypeController> logger) : ControllerBase
    {
        /// <summary>Retrieves all shoe-type.</summary>
        /// <remarks>Returns every shoe-type currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of shoe-type, or no content if no shoe-type exist.</returns>
        /// <response code="200">Returns the list of shoe-type.</response>
        /// <response code="204">No shoe-type were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<ShoeTypeResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/shoe-types - request to get all shoe-type");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0)
                {
                    logger.LogWarning("GET /api/shoe-types - no shoe-type found");
                    return NoContent();
                }

                logger.LogInformation("GET /api/shoe-types - returned {Count} shoe-type", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/shoe-type");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>Retrieves a shoe-type by its identifier.</summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShoeTypeResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/shoe-types/{Id} - request to get shoe-type by id", id);
            try
            {
                var result = await service.GetByIdAsync(id);
                logger.LogInformation("GET /api/shoe-types/{Id} - shoe-type found: {@ShoeType}", id, result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "GET /api/shoe-types/{Id} - shoe-type not found", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/shoe-types/{Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] ShoeTypeRequest shoeTypeRequest)
        {
            logger.LogInformation("POST /api/shoe-types - attempt to add shoe-type: {@ShoeTypeRequest}",
                shoeTypeRequest);
            try
            {
                var result = await service.AddAsync(shoeTypeRequest);
                logger.LogInformation("POST /api/shoe-types - shoe-type added successfully");
                return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
            }
            catch (UniqueObjectException ex)
            {
                logger.LogWarning(ex, "POST /api/shoe-types - shoe-type is already exists");
                return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in POST /api/shoe-types");
                return BadRequest();
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, ShoeTypeRequest obj)
        {
            logger.LogInformation("PUT /api/shoe-types - attempt to update shoe-type: {@ShoeTypeRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/shoe-types - shoe-type updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/shoe-types - shoe-type not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/shoe-types");
                return BadRequest();
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/shoe-types/{Id} - attempt to delete shoe-type", id);
            try
            {
                ShoeTypeResponse result = await service.DeleteAsync(id);
                logger.LogInformation("DELETE /api/shoe-types/{Id} - shoe-type deleted successfully", id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "DELETE /api/shoe-types/{Id} - shoe-type not found", id);
                return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DELETE /api/shoe-types/{Id}", id);
                return BadRequest();
            }
        }
    }
}