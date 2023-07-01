using IdentityModel;
using Microsoft.AspNetCore.Identity;
using ShopApp.IdentityServer.IdentityServerData.Models;
using System.Security.Claims;

namespace ShopApp.IdentityServer.IdentityServerData
{
    public static class DbInitializer
    {
        public static async void Initialize(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                await roleManager.CreateAsync(new IdentityRole(SD.Admin));
                await roleManager.CreateAsync(new IdentityRole(SD.Customer));
            }
            else { return; }

            ApplicationUser adminUser = new()
            {
                UserName = "admin@promoland.ca",
                Email = "admin@promoland.ca",
                EmailConfirmed = true,
                PhoneNumber = "0021650793864",
                FirstName = "Idriss",
                LastName = "Laabidi"
            };

            await userManager.CreateAsync(adminUser, "Password1!");
            await userManager.AddToRoleAsync(adminUser, SD.Admin);

            await userManager.AddClaimsAsync(adminUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,adminUser.FirstName+" "+ adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Admin),
            });

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "mouez@promoland.ca",
                Email = "mouez@promoland.ca",
                EmailConfirmed = true,
                PhoneNumber = "0021627599969",
                FirstName = "Mouez",
                LastName = "Gharbi"
            };

            await userManager.CreateAsync(customerUser, "Password1!");
            await userManager.AddToRoleAsync(customerUser, SD.Customer);

            await userManager.AddClaimsAsync(customerUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,customerUser.FirstName+" "+ customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Customer),
            });
        }
    }
}
