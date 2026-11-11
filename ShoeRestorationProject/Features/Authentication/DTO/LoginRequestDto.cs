using System.ComponentModel.DataAnnotations;

namespace ShoeRestorationProject.Features.Authentication.DTO;

public class LoginRequestDto
{
    [Required]
    [MaxLength(64)]
    public string Username { get; set; } = null!;
    [Required]
    [MaxLength(128)]
    public string Password { get; set; } = null!;
}