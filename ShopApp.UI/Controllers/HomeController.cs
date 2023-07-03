using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.UI.Models;
using ShopApp.UI.Models.Dtos;
using ShopApp.UI.Services;
using System.Diagnostics;

namespace ShopApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger,IConfiguration config,
            IProductService productService)
        {
            _logger = logger;
            _config = config;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new();
            var response = await _productService.GetAllProducts<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(
                    Convert.ToString(response.Result));
            }
                return View(products);
        }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}