using ShopApp.UI.Models;
using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IHttpClientFactory httpClient) : base(httpClient) { }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            //var link = $"https://localhost:7055/api/cartGetCart/{userId}";
            return await SendAsync<T>(new ApiRequest()
            {
                
                ApiType = SD.ApiType.GET,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + $"/api/cart/GetCart/{userId}",
                AccessToken = token
            }) ;
        }

        public async Task<T> AddCartAsync<T>(CartDto cartDto, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/AddCart";
            return await SendAsync<T>(new ApiRequest()
            {
                
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/UpdateCart";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                //Url=link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/UpdateCart",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartDetailsId, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/RemoveFromCart";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/RemoveFromCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCountAsync<T>(CountDetailsDto count, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/UpdateCount";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = count,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/UpdateCount",
                AccessToken = token
            });
        }

        public async Task<T> ClearCartAsync<T>(string userId, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/ClearCart";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userId,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/ClearCart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/ApplyCoupon";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveCouponAsync<T>(string userId, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/RemoveCoupon";
            return await SendAsync<T>(new ApiRequest()
            {
                
                ApiType = SD.ApiType.POST,
                Data = userId,
                //Url=link,
               Url = SD.ShoppingCartAPIUrl + "/api/cart/RemoveCoupon",
                AccessToken = token
            });
        }

        public async Task<T> CheckoutAsync<T>(CartHeaderDto cartHeader, string token = null)
        {
            //var link = "https://localhost:7055/api/cart/Checkout";
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartHeader,
                //Url = link,
                Url = SD.ShoppingCartAPIUrl + "/api/cart/Checkout",
                AccessToken = token
            });
        }
    }
}
