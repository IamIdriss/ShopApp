using Matgr.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopApp.ShoppingCartAPI.ShoppingCartData
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
