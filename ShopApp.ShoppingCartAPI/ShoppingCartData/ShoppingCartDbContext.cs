using Microsoft.EntityFrameworkCore;

namespace ShopApp.ShoppingCartAPI.ShoppingCartData
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
