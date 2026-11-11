using Microsoft.AspNetCore.Mvc;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.Authentication.DTO;
using ShoeRestorationProject.Features.Users;

namespace ShoeRestorationProject.Features.Authentication;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    private void SetJwtCookie(string token, DateTime expiresAt)
    {
        Response.Cookies.Append("jwt-token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = expiresAt,
            Path = "/api"
        });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequestDto loginRequestDto)
    {
        try
        {
            var (token, expiresAt) =  await _authenticationService.LoginAsync(loginRequestDto);
            SetJwtCookie(token, expiresAt);
            return Ok(new { token, expiresAt });
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        try
        {
            var registerResponseDto = await _authenticationService.RegisterAsync(registerRequestDto);
            
            SetJwtCookie(registerResponseDto.JwtResponseDto.Token,
                registerResponseDto.JwtResponseDto.ExpiresAt);
            
            return CreatedAtAction(nameof(UserController.GetByIdAsync), 
                "User", new { id = registerResponseDto.Id },
                new { registerResponseDto.JwtResponseDto.Token,
                    registerResponseDto.JwtResponseDto.ExpiresAt });
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
    }
}