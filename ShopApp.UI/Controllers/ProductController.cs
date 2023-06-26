using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.UI.Models.Dtos;
using ShopApp.UI.Services;
using System.Collections.Generic;

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
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            
            var response = await _productService.GetProduct<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductDto model)
        {
            if (ModelState.IsValid)
            {
              
                var response = await _productService.UpdateProduct<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            
            var response = await _productService.GetProduct<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDto model)
        {
            
            var response = await _productService.DeleteProduct<ResponseDto>(model.ProductId);
            if (response != null && response.IsSuccess)
            {
               
                return RedirectToAction("ProductIndex");
            }
            return View(model);
        }
    }
}
