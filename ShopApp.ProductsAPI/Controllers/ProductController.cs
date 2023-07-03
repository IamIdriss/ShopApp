using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
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

        //Insert New Product /api/products
        [HttpPost]
        [Authorize]
        public async Task<object> CreateProduct(ProductDto productDto)
        {
            try
            {
                if (productDto.ProductId == 0)
                {
                    ProductDto model = await _productRepository.UpsertProduct(productDto);

                    _response.Result = model;
                    _response.DisplayMessage = "Product has been created";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error occurs while creating product!";
                    _response.ErrorMessages = new() { "Error occurs while creating product!" };
                    return _response;
                }

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }

            return _response;
        }

        //Update Product /api/products
        [HttpPut]
        [Authorize]
        public async Task<object> UpdateProduct(ProductDto productDto)
        {
            try
            {
                if (productDto.ProductId > 0)
                {
                    ProductDto model = await _productRepository.UpsertProduct(productDto);

                    _response.Result = model;
                    _response.DisplayMessage = "Product has been updated";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Product id is not correct!";
                    _response.ErrorMessages = new() { "Product id is not correct!" };
                    return _response;
                }

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }

            return _response;
        }

        //Delete Product /api/products/1
        [HttpDelete]
        [Authorize (Roles ="Admin")]
        [Route("{id}")]
        public async Task<object> DeleteProduct(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                if (!isSuccess)
                {
                    _response.IsSuccess = false;
                    _response.Result = isSuccess;
                    _response.DisplayMessage = "Product is not found!";
                    _response.ErrorMessages = new() { "Product is not found!" };
                    return _response;
                }
                else
                {
                    _response.Result = isSuccess;
                    _response.DisplayMessage = "Product has been deleted";
                }

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
