using Microsoft.AspNetCore.Identity;

namespace ShopApp.IdentityServer.IdentityServerData.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
