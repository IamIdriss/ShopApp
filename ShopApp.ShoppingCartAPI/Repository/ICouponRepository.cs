using ShopApp.ShoppingCartAPI.Models.Dtos;

namespace ShopApp.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
