using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FansPen.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace FansPen.Domain
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@fp.net";
            string password = "Admin_1";
            string adminUsername = "admin";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("ban") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("ban"));
            }
            if (await userManager.FindByNameAsync(adminUsername) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminUsername, EmailConfirmed = true,
                    AboutMe = adminUsername, FirstName = adminUsername, SecondName = adminUsername,
                    AvatarUrl = "http://res.cloudinary.com/fanspen/image/upload/t_avatarMain/retzxmy2g9bqn9uqqqpt.png" };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
