using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;

namespace ShoeRestorationProject.Features.Roles;

[ApiController]
[Route("/api/roles")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<RoleResponse>>> GetAllAsync()
    {
        var result = await _roleService.GetAllAsync();
        if (result.Count == 0)
            return NoContent();
        return Ok(result);
    }
    
    [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
    public async Task<ActionResult<RoleResponse>> GetByIdAsync(int id)
    {
        try
        {
            var result = await _roleService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<RoleResponse>> AddAsync(RoleRequest roleRequest)
    {
        try
        {
            var result = await _roleService.AddAsync(roleRequest);
            return CreatedAtAction(actionName: nameof(GetByIdAsync),
                routeValues: new {id = result.Id},
                value: result);
        }
        catch (UniqueObjectException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<RoleResponse>> DeleteAsync(int id)
    {
        try
        {
            var result = await _roleService.DeleteAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<RoleResponse>> UpdateAsync(int id, RoleRequest roleRequest)
    {
        try
        {
            var result = await _roleService.UpdateAsync(id, roleRequest);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}