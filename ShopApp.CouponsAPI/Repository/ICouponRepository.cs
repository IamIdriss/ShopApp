using ShopApp.CouponsAPI.Models.Dtos;

namespace ShopApp.CouponsAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
