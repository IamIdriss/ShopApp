using Microsoft.EntityFrameworkCore;
using ShopApp.OrdersAPI.Models;
using ShopApp.OrdersAPI.OrdersAPIData;

namespace ShopApp.OrdersAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<OrdersDbContext> _context;

        public OrderRepository(DbContextOptions<OrdersDbContext> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var _dbContext = new OrdersDbContext(_context);
            _dbContext.OrderHeaders.Add(orderHeader);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool status)
        {
            await using var _dbContext = new OrdersDbContext(_context);
            var orderHeaderFromDb = await _dbContext.OrderHeaders
                .FirstOrDefaultAsync(oh => oh.OrderHeaderId == orderHeaderId);
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = status;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
