using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.UI.Models.Dto;
using ShopApp.UI.Models.Dtos;
using ShopApp.UI.Services;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace ShopApp.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContext;

        public CartController(IProductService productService,ICartService cartService,
            IHttpContextAccessor httpContext)
        {
            _productService = productService;
            _cartService = cartService;
            _httpContext = httpContext;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                _httpContext.HttpContext.Session.SetInt32("count", GetCartCount().Result);
            }
            return View(await LoadCartOfLoggedInUser());
        }

        public async Task<IActionResult> RemoveItem(int CartDetailId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(CartDetailId, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("CartIndex");
            }
            return View();
        }

        public async Task<IActionResult> Decrease(int DetailId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            CountDetailsDto count = new()
            {
                CartDetailsId = DetailId,
                Action = "decrement",
                Amount = 1
            };
            var response = await _cartService
                                .UpdateCountAsync<ResponseDto>(count, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("CartIndex");
            }

            return RedirectToAction("CartIndex"); ;
        }

        public async Task<IActionResult> Increase(int DetailId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            CountDetailsDto count = new()
            {
                CartDetailsId = DetailId,
                Action = "increment",
                Amount = 1
            };
            var response = await _cartService
                                .UpdateCountAsync<ResponseDto>(count, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("CartIndex");
            }

            return RedirectToAction("CartIndex"); ;
        }

        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService
                                .ClearCartAsync<ResponseDto>(userId, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("CartIndex");
            }

            return RedirectToAction("CartIndex"); ;
        }

        private async Task<CartDto> LoadCartOfLoggedInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService
                .GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

            CartDto cart = new();

            if (response != null && response.IsSuccess)
            {
                cart = JsonConvert
                    .DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if (cart.CartHeader != null)
            {
                foreach (var detail in cart.CartDetails)
                {
                    cart.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }
            }

            return cart;
        }

        public async Task<int> GetCartCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService
                .GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

            CartDto cart = new();

            if (response != null && response.IsSuccess)
            {
                cart = JsonConvert
                    .DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }
            int count = 0;
            if (cart.CartHeader != null)
            {
                count = (int)cart.CartDetails.Select(cd => cd.Count).Sum();
            }
            return count;
        }

    }
}
