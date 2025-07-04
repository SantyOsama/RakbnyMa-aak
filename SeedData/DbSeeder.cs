using Microsoft.AspNetCore.Identity;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.SeedData
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(UserType)))
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    await roleManager.CreateAsync(role);
                }
            }
        }
    }

}
