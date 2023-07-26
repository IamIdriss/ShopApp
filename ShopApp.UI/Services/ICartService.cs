using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cartDetailsId, string token = null);
        Task<T> UpdateCountAsync<T>(CountDetailsDto count, string token = null);
        Task<T> ClearCartAsync<T>(string userId, string token = null);
        Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveCouponAsync<T>(string userId, string token = null);
        Task<T> CheckoutAsync<T>(CartHeaderDto cartHeader, string token = null);
    }
}
