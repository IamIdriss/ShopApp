using Microsoft.EntityFrameworkCore;
using ShopApp.ProductsAPI.Models;

namespace ShopApp.ProductsAPI.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
