using Microsoft.EntityFrameworkCore;

namespace ShopApp.CouponsAPI.CouponData
{
    public class CouponDbContext : DbContext
    {
        public CouponDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
