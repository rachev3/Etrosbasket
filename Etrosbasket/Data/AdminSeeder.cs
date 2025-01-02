using Microsoft.AspNetCore.Identity;

public class AdminSeeder
{
    public static async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create Admin Role
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Check if Admin User exists
        var adminUser = await userManager.FindByEmailAsync("ddrachev123@gmail.com");
        if (adminUser == null)
        {
            // Create the admin user
            adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "ddrachev123@gmail.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Sorte3202?"); // Set a strong password
            if (!result.Succeeded)
            {
                // Log errors and stop execution
                throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // Add the user to the Admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!roleResult.Succeeded)
            {
                // Log errors and stop execution
                throw new Exception("Failed to add admin user to Admin role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }
        }
    }
}
