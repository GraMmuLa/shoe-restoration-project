using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Services;

namespace ShoeRestorationProject.Features.Countries;

[ApiController]
[Route("api/[controller]")]
public class CountryController(CountryService service, ILogger<CountryController> logger) : ControllerBase
{
    /// <summary>Retrieves all country.</summary>
    /// <remarks>Returns every country currently available. If the collection is empty, the endpoint responds with 204 No Content.</remarks>
    /// <returns>The list of country, or no content if no country exist.</returns>
    /// <response code="200">Returns the list of country.</response>
    /// <response code="204">No country were found.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet]
    public async Task<ActionResult<IList<CountryResponse>>> GetAllAsync()
    {
        logger.LogInformation("GET /api/countries - request to get all country");
        try
        {
            var result = await service.GetAllAsync();
            if (result.Count == 0) { logger.LogWarning("GET /api/countries - no country found"); return NoContent(); }
            logger.LogInformation("GET /api/countries - returned {Count} country", result.Count);
            return Ok(result);
        }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/country"); return StatusCode(500, "Internal server error."); }
    }

    /// <summary>Retrieves a country by its identifier.</summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryResponse>> GetByIdAsync(int id)
    {
        logger.LogInformation("GET /api/countries/{Id} - request to get country by id", id);
        try { var result = await service.GetByIdAsync(id); logger.LogInformation("GET /api/countries/{Id} - country found: {@Country}", id, result); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "GET /api/countries/{Id} - country not found", id); return Problem(statusCode: StatusCodes.Status404NotFound, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in GET /api/countries/{Id}", id); return StatusCode(500, "Internal server error."); }
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm] CountryRequest country)
    {
        logger.LogInformation("POST /api/countries - attempt to add country: {@CountryRequest}", country);
        try { var result = await service.AddAsync(country); logger.LogInformation("POST /api/countries - country added successfully"); return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result); }
        catch (UniqueObjectException ex) { logger.LogWarning(ex, "POST /api/countries - country is already exists"); return Problem(statusCode: StatusCodes.Status409Conflict, title: ex.Message); }
        catch (Exception ex) { logger.LogError(ex, "Error in POST /api/countries"); return BadRequest(); }
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, CountryRequest obj)
    {
        logger.LogInformation("PUT /api/countrys - attempt to update country: {@ CountryRequest}", obj);
        try
        {
            var result = await service.UpdateAsync(id, obj);
            logger.LogInformation("PUT /api/countrys - country updated successfully");
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "PUT /api/countrys - country not found");
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in PUT /api/countrys");
            return BadRequest();
        }
    }


    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        logger.LogInformation("DELETE /api/countries/{Id} - attempt to delete country", id);
        try { CountryResponse result = await service.DeleteAsync(id); logger.LogInformation("DELETE /api/countries/{Id} - country deleted successfully", id); return Ok(result); }
        catch (NotFoundException ex) { logger.LogWarning(ex, "DELETE /api/countries/{Id} - country not found", id); return Problem(title: ex.Message, statusCode: StatusCodes.Status404NotFound); }
        catch (Exception ex) { logger.LogError(ex, "Error in DELETE /api/countries/{Id}", id); return BadRequest(); }
    }
}