using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.Jwt;
using ShoeRestorationProject.Features.Users.DTO;

namespace ShoeRestorationProject.Features.Users;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<ActionResult<IList<UserDto>>> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();
        return users.Count == 0 ? NoContent() : Ok(users);
    }

    [HttpGet("{id:int}"), ActionName("GetByIdAsync")]
    public async Task<ActionResult<UserDto>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}