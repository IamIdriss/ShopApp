using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopApp.IdentityServer.IdentityServerData.Models;

namespace ShopApp.IdentityServer.IdentityServerData
{
    public class IdentityServerDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityServerDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
