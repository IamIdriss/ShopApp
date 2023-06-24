using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.ProductsAPI.Models.Dtos;
using ShopApp.ProductsAPI.Repository;

namespace ShopApp.ProductsAPI.Controllers
{
    [Route("api/prodcuts")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private ProductResponseDto _response;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _response = new ProductResponseDto();
        }
        //Get All Products /api/products
        [HttpGet]
        public async Task<ProductResponseDto> GetAllProducts()
        {
            try
            {
                IEnumerable<ProductDto> productDtos =
                    await _productRepository.GetProducts();

                _response.Result = productDtos;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }

            return _response;
        }

        //Get Single Product /api/products/1
        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetProduct(int id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);

                _response.Result = productDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }

            return _response;
        }
    }
}
