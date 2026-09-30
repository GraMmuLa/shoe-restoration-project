using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;
using ShoeRestorationProject.Services;
using Microsoft.Extensions.Logging;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly BrandService _service;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(BrandService service, ILogger<BrandsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all brands.
        /// </summary>
        /// <remarks>Returns every brand currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of brands, or no content if no brands exist.</returns>
        /// <response code="200">Returns the list of brands.</response>
        /// <response code="204">No brands were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<BrandDto>>> GetAllAsync()
        {
            _logger.LogInformation("GET /api/brands - request to get all brands.");
            try
            {
                var result = await _service.GetAllAsync();
                if (result.Count == 0)
                {
                    _logger.LogWarning("GET /api/brands - no brands found.");
                    return NoContent();
                }
                _logger.LogInformation("GET /api/brands - returned {Count} brands.", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GET /api/brands.");
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
        public async Task<ActionResult<BrandDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("GET /api/brands/{Id} - request to get brand by id", id);
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("GET /api/brands/{Id} - brand not found", id);
                    return NotFound();
                }
                _logger.LogInformation("GET /api/brands/{Id} - brand found: {@Brand}", id, result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GET /api/brands/{Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Creates a brand using the submitted form data.
        /// </summary>
        /// <remarks>Submit the brand fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="brand">The brand data to create.</param>
        /// <returns>An empty success response when the brand is created.</returns>
        /// <response code="200">The brand was created successfully.</response>
        /// <response code="400">The brand could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] BrandDto brand)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("POST /api/brands - invalid model state. Errors: {Errors}",
                    ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            _logger.LogInformation("POST /api/brands - attempt to add brand: {@BrandDto}", brand);
            try
            {
                await _service.AddAsync(brand);
                _logger.LogInformation("POST /api/brands - brand added successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in POST /api/brands");
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(BrandDto obj)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("PUT /api/brands - invalid model state. Errors: {Errors}",
                    ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            _logger.LogInformation("PUT /api/brands - attempt to update brand: {@BrandDto}", obj);
            try
            {
                _service.Update(obj);
                _logger.LogInformation("PUT /api/brands - brand updated successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PUT /api/brands");
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
            _logger.LogInformation("DELETE /api/brands/{Id} - attempt to delete brand", id);
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity is null)
                {
                    _logger.LogWarning("DELETE /api/brands/{Id} - brand not found", id);
                    return NotFound();
                }

                _service.Delete(entity);
                _logger.LogInformation("DELETE /api/brands/{Id} - brand deleted successfully", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DELETE /api/brands/{Id}", id);
                return BadRequest();
            }
        }
    }
}
