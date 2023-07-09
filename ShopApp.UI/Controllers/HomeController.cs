using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.UI.Models;
using ShopApp.UI.Models.Dto;
using ShopApp.UI.Models.Dtos;
using ShopApp.UI.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace ShopApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContext;

        public HomeController(ILogger<HomeController> logger,IConfiguration config,
            IProductService productService,ICartService cartService,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _config = config;
            _productService = productService;
            _cartService = cartService;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new();
            var response = await _productService.GetAllProducts<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(
                    Convert.ToString(response.Result));
                if (User.Identity.IsAuthenticated)
                {
                    _httpContext.HttpContext.Session.SetInt32("count", GetCartCount().Result);
                }


            }
                return View(products);
        }
        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto product = new();
            var response = await _productService.GetProduct<ResponseDto>(productId, "");
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(
                    Convert.ToString(response.Result));
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto()
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                }
            };

            CartDetailsDto cartDetailsDto = new()
            {
                Count = productDto.Quantity,
                ProductId = productDto.ProductId,
            };

            var response = await _productService
                .GetProduct<ResponseDto>(productDto.ProductId, "");
            if (response != null && response.IsSuccess)
            {
                cartDetailsDto.Product = JsonConvert
                    .DeserializeObject<ProductDto>(response.Result.ToString());
            }

            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetailsDto);

            cartDto.CartDetails = cartDetailsDtos;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResponse = await _cartService
                .AddCartAsync<ResponseDto>(cartDto, accessToken);

            if (addToCartResponse != null && addToCartResponse.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(productDto);
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies","oidc");
        }
        public IActionResult Register()
        {
            string url = _config["APIUrls:IdentityServer"];
            return Redirect($"{url}/Account/Register/Index");
        }

        public IActionResult Privacy()
        {
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}