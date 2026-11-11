using Microsoft.AspNetCore.Identity;
using ShoeRestorationProject.Features.Roles;

namespace ShoeRestorationProject.Features.Authorization;

public class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles =
        {
            AppRoles.Admin,
            AppRoles.User
        };

        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role)) continue;
            
            var identityResult =
                await roleManager.CreateAsync(new IdentityRole(role));

            if (!identityResult.Succeeded)
                throw new InvalidOperationException(string.Join(", ",
                    identityResult.Errors.Select(e => e.Description)));
        }
    }
}