using Microsoft.EntityFrameworkCore;

namespace ShopApp.ProductsAPI.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) 
        {
            
        }
    }
}
