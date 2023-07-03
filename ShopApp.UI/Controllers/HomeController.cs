using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.UI.Models;
using System.Diagnostics;

namespace ShopApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
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