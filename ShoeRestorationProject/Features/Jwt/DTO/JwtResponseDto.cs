namespace ShoeRestorationProject.Features.Jwt.DTO;

public record JwtResponseDto(string Token, DateTime ExpiresAt);