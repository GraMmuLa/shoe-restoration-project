using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.Brands
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController(BrandService service,
        ILogger<BrandController> logger) : ControllerBase
    {
        /// <summary>
        /// Retrieves all brand.
        /// </summary>
        /// <remarks>Returns every brand currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of brand, or no content if no brand exist.</returns>
        /// <response code="200">Returns the list of brand.</response>
        /// <response code="204">No brand were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<BrandResponse>>> GetAllAsync()
        {
            logger.LogInformation("GET /api/brands - request to get all brand");
            try
            {
                var result = await service.GetAllAsync();
                if (result.Count == 0)
                {
                    logger.LogWarning("GET /api/brands - no brand found");
                    return NoContent();
                }
                logger.LogInformation("GET /api/brands - returned {Count} brand", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/brand");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Retrieves a brand by its identifier.
        /// </summary>
        /// <remarks>Looks up the brand using the identifier in the route. A brand that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the brand to retrieve.</param>
        /// <returns>The requested brand when found.</returns>
        /// <response code="200">Returns the requested brand.</response>
        /// <response code="404">No brand exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BrandResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("GET /api/brands/{Id} - request to get brand by id", id);
            try
            {
                var result = await service.GetByIdAsync(id);

                logger.LogInformation("GET /api/brands/{Id} - brand found: {@Brand}", id, result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "GET /api/brands/{Id} - brand not found", id);
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET /api/brands/{Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Creates a brand using the submitted form data.
        /// </summary>
        /// <remarks>Submit the brand fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="brand">The brand data to create.</param>
        /// <returns>An empty success response when the brand is created.</returns>
        /// <response code="201">The brand was created successfully.</response>
        /// <response code="400">The brand could not be created.</response>
        /// <response code="409">The brand already exists.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] BrandRequest brand)
        {
            logger.LogInformation("POST /api/brands - attempt to add brand: {@BrandRequest}", brand);
            try
            {
                var result = await service.AddAsync(brand);
                logger.LogInformation("POST /api/brands - brand added successfully");
                return CreatedAtRoute(nameof(GetByIdAsync),new { id = result.Id }, result);
            }
            catch (UniqueObjectException ex)
            {
                logger.LogWarning(ex, "POST /api/brands - brand is already exists");
                return Problem(
                    statusCode: StatusCodes.Status409Conflict,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in POST /api/brands");
                return BadRequest();
            }
        }

        /// <summary>
        /// Update a brand
        /// </summary>
        /// <remarks>Submit the updated brand object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated brand data, including the identifier of the brand to update.</param>
        /// <returns>An empty success response when the brand is updated.</returns>
        /// <response code="200">The brand was updated successfully.</response>
        /// <response code="400">The brand could not be updated.</response>
        /// <response code="404">The brand was not found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
                [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, BrandRequest obj)
        {
            logger.LogInformation("PUT /api/brands - attempt to update brand: {@ BrandRequest}", obj);
            try
            {
                var result = await service.UpdateAsync(id, obj);
                logger.LogInformation("PUT /api/brands - brand updated successfully");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "PUT /api/brands - brand not found");
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in PUT /api/brands");
                return BadRequest();
            }
        }


        /// <summary>
        /// Delete a brand by id
        /// </summary>
        /// <remarks>The endpoint first checks whether the brand exists, then deletes it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the brand to delete.</param>
        /// <returns>An empty success response when the brand is deleted.</returns>
        /// <response code="200">The brand was deleted successfully.</response>
        /// <response code="404">No brand exists with the specified identifier.</response>
        /// <response code="400">The brand could not be deleted.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("DELETE /api/brands/{Id} - attempt to delete brand", id);
            try
            {
                var result = await service.DeleteAsync(id);
                
                logger.LogInformation("DELETE /api/brands/{Id} - brand deleted successfully", id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, "DELETE /api/brands/{Id} - brand not found", id);
                return Problem(
                    title: ex.Message,
                    statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DELETE /api/brands/{Id}", id);
                return BadRequest();
            }
        }
    }
}
