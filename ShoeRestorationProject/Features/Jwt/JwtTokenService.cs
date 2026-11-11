using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ShoeRestorationProject.Features.Jwt.DTO;
using ShoeRestorationProject.Features.Users;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ShoeRestorationProject.Features.Jwt;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _options;
    private  readonly JsonWebTokenHandler _tokenHandler = new();
    
    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public JwtResponseDto GenerateToken(JwtUserDto user)
    {
        var expiresAt = DateTime.Now.AddMinutes(_options.ExpirationMinutes);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, user.RoleName)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            Expires = expiresAt,
            SigningCredentials = credentials
        };
        
        var token = _tokenHandler.CreateToken(descriptor);
        

        return new JwtResponseDto(token, expiresAt);
    }
}