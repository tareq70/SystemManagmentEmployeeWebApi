using Microsoft.AspNetCore.Identity;

namespace SystemManagmentEmployeeWebApi.Seeding
{
    public class SeedRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Manager"))
                await roleManager.CreateAsync(new IdentityRole("Manager"));

            if (!await roleManager.RoleExistsAsync("HR"))
                await roleManager.CreateAsync(new IdentityRole("HR"));

        }

    }
}
