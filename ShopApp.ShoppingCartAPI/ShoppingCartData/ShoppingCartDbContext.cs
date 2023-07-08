
using Microsoft.EntityFrameworkCore;
using ShopApp.ShoppingCartAPI.Models;

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
