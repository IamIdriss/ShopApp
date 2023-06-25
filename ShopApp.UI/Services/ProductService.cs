using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> CreateProduct<T>(ProductDto productDto)
        {
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType=SD.ApiType.POST,
                Data= productDto,
                Url=SD.ProductsAPIUrl+"/api/products",
                AccessToken=""
            }
                );
        }

        public async Task<T> DeleteProduct<T>(int id)
        {
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
              
                Url = SD.ProductsAPIUrl + $"/api/products/{id}",
                AccessToken = ""
            }
                );
        }

        public async Task<T> GetAllProducts<T>()
        {
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ProductsAPIUrl + "/api/products",
                AccessToken = ""
            }
                );
        }

        public async Task<T> GetProduct<T>(int id)
        {
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ProductsAPIUrl + $"/api/products/{id}",
                AccessToken = ""
            }
                );
        }

        public async Task<T> UpdateProduct<T>(ProductDto productDto)
        {
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductsAPIUrl + "/api/products",
                AccessToken = ""
            }
                );
        }
    }
}
