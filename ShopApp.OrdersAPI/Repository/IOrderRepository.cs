using ShopApp.OrdersAPI.Models;

namespace ShopApp.OrdersAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool status);
    }
}
