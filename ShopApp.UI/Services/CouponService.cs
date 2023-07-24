using ShopApp.UI.Models;

namespace ShopApp.UI.Services
{
    public class CouponService : BaseService,ICouponService
    {
        public CouponService(IHttpClientFactory httpClient) : base(httpClient) { }

        public async Task<T> GetCouponAsync<T>(string couponCode, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponsAPIUrl + $"/api/coupon/{couponCode}",
                AccessToken = token
            });
        }
    }
}
