using ShoeRestorationProject.Features.Jwt;
using ShoeRestorationProject.Features.Jwt.DTO;

namespace ShoeRestorationProject.Features.Authentication.DTO;

public record RegisterResponseDto(int Id, JwtResponseDto JwtResponseDto);