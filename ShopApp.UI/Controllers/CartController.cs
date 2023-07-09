using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.UI.Models.Dto;
using ShopApp.UI.Models.Dtos;
using ShopApp.UI.Services;
using System.Security.Claims;

namespace ShopApp.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService,ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartOfLoggedInUser());
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

    }
}
