using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.UI.Models.Dtos;
using ShopApp.UI.Services;

namespace ShopApp.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> products = new ();
            var response = await _productService.GetAllProducts<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(products);
        }
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var resoponse =await _productService.CreateProduct<ResponseDto>(model);
                if (resoponse!=null && resoponse.IsSuccess)
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            return View(model); 
        }
    }
}
