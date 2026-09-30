using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.DTO;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController(CountryService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all countries.
        /// </summary>
        /// <remarks>Returns every country currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
        /// <returns>The list of countries, or no content if no countries exist.</returns>
        /// <response code="200">Returns the list of countries.</response>
        /// <response code="204">No countries were found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult<IList<CountryDto>>> GetAllAsync()
        {
            var result = await service.GetAllAsync();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a country by its identifier.
        /// </summary>
        /// <remarks>Looks up the country using the identifier in the route. A country that does not exist produces a 404 Not Found response.</remarks>
        /// <param name="id">The identifier of the country to retrieve.</param>
        /// <returns>The requested country when found.</returns>
        /// <response code="200">Returns the requested country.</response>
        /// <response code="404">No country exists with the specified identifier.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetByIdAsync(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a country using the submitted form data.
        /// </summary>
        /// <remarks>Submit the country fields as form data. The endpoint returns 200 OK when creation succeeds and 400 Bad Request if creation fails.</remarks>
        /// <param name="obj">The country data to create.</param>
        /// <returns>An empty success response when the country is created.</returns>
        /// <response code="200">The country was created successfully.</response>
        /// <response code="400">The country could not be created.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] CountryDto obj)
        {
            try
            {
                await service.AddAsync(obj);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates an existing country using the submitted data.
        /// </summary>
        /// <remarks>Submit the updated country object in the request body, including its identifier. The endpoint returns 200 OK when the update succeeds and 400 Bad Request if it fails.</remarks>
        /// <param name="obj">The updated country data, including the identifier of the country to update.</param>
        /// <returns>An empty success response when the country is updated.</returns>
        /// <response code="200">The country was updated successfully.</response>
        /// <response code="400">The country could not be updated.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult Update(CountryDto obj)
        {
            try
            {
                service.Update(obj);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a country by its identifier.
        /// </summary>
        /// <remarks>The endpoint checks whether the country exists before deleting it. It returns 404 Not Found when the identifier is unknown, 200 OK on success, or 400 Bad Request if deletion fails.</remarks>
        /// <param name="id">The identifier of the country to delete.</param>
        /// <returns>An empty success response when the country is deleted.</returns>
        /// <response code="200">The country was deleted successfully.</response>
        /// <response code="404">No country exists with the specified identifier.</response>
        /// <response code="400">The country could not be deleted.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var entity = await service.GetByIdAsync(id);
                if (entity is null)
                    return NotFound();

                service.Delete(entity);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
