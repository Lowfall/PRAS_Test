using Microsoft.AspNetCore.Identity;
using Newspaper.Domain.Entities;

namespace Newspaper.Web.Infrastructure.Helpers;

public static class DataSeeder
{
    private static readonly List<(string, string)> Admins = new()
    {
        ("admin1@gmail.com", "AdminPassword1!"),
        ("admin2@gmail.com", "AdminPassword2!"),
        ("admin3@gmail.com", "AdminPassword3!"),
    };
    
    public static async Task InitializeAdminsAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        foreach (var admin in Admins)
        {
            var adminUser = await userManager.FindByEmailAsync(admin.Item1);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = admin.Item1,
                    Email = admin.Item1,
                    EmailConfirmed = true
                };
                
                var result = await userManager.CreateAsync(adminUser, admin.Item2);
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}