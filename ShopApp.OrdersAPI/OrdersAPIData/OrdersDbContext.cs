using Microsoft.EntityFrameworkCore;
using ShopApp.OrdersAPI.Models;

namespace ShopApp.OrdersAPI.OrdersAPIData
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
