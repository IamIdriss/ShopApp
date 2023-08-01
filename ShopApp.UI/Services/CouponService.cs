using ShopApp.UI.Models;

namespace ShopApp.UI.Services
{
    public class CouponService : BaseService,ICouponService
    {
        public CouponService(IHttpClientFactory httpClient) : base(httpClient) { }

        public async Task<T> GetCouponAsync<T>(string couponCode, string token = null)
        {
            var link = $"https://localhost:7041/api/coupon/{couponCode}";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url= link,
                //Url = SD.CouponsAPIUrl + $"/api/coupon/{couponCode}",
                AccessToken = token
            });
        }
    }
}
