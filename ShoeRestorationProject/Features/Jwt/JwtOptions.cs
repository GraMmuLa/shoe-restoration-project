using System.Text.Json.Serialization;

namespace ShoeRestorationProject.Features.Jwt;

public class JwtOptions
{
    public const string Section = "Jwt";

    [JsonPropertyName("Audience")]
    public string Audience { get; init; } = string.Empty;
    [JsonPropertyName("Issuer")]
    public string Issuer { get; init; } = string.Empty;
    [JsonPropertyName("SecretKey")]
    public string SecretKey { get; init; } = string.Empty;

    public int ExpirationMinutes { get; init; } = 30;
}