using ShoeRestorationProject.Features.Jwt.DTO;
using ShoeRestorationProject.Features.Users;

namespace ShoeRestorationProject.Features.Jwt;

public interface IJwtTokenService
{
    public JwtResponseDto GenerateToken(JwtUserDto userDto);
}