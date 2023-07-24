using ShopApp.UI.Models;
using ShopApp.UI.Models.Dto;
using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IHttpClientFactory httpClient) : base(httpClient) { }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIUrl + $"/api/cart/GetCart/{userId}",
                AccessToken = token
            });
        }

        public async Task<T> AddCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/UpdateCart",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartDetailsId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/RemoveFromCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCountAsync<T>(CountDetailsDto count, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = count,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/UpdateCount",
                AccessToken = token
            });
        }

        public async Task<T> ClearCartAsync<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userId,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/ClearCart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveCouponAsync<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userId,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/RemoveCoupon",
                AccessToken = token
            });
        }

        public async Task<T> CheckoutAsync<T>(CartHeaderDto cartHeader, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartHeader,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/Checkout",
                AccessToken = token
            });
        }
    }
}
