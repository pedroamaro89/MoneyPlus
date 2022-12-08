using Microsoft.AspNetCore.Identity;
using MoneyPlus.Data;

namespace MoneyPlus.Areas.Identity.Data
{
    public class CreateRoles
    {
        public static async Task Initialize(MoneyPlusContext context,
        RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            string adminRole = "Admin";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
        }
    }
}
