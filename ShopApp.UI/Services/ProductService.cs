using ShopApp.UI.Models;
using ShopApp.UI.Models.Dtos;
using static System.Net.WebRequestMethods;

namespace ShopApp.UI.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> CreateProduct<T>(ProductDto productDto)
        {
            var link = "https://localhost:7055/api/prodcuts";
            return await SendAsync<T>(new ApiRequest()
            {
                
                ApiType =SD.ApiType.POST,
                Data= productDto,
                Url=link,
                //Url=SD.ProductsAPIUrl+"/api/products",
                AccessToken=""
            }
                );
        }

        public async Task<T> DeleteProduct<T>(int id)
        {
            var link = $"https://localhost:7055/api/prodcuts/{id}";
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url=link,
              
                //Url = SD.ProductsAPIUrl + $"/api/products/{id}",
                AccessToken = ""
            }
                );
        }

        public async Task<T> GetAllProducts<T>()
        {
            var link ="https://localhost:7055/api/prodcuts";
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = link,
                //Url = SD.ProductsAPIUrl + "/api/products",

                AccessToken = ""
            }
                ) ; 
            
        }

        public async Task<T> GetProduct<T>(int id)
        {
            var link = $"https://localhost:7055/api/prodcuts/{id}";
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = link,

                //Url = SD.ProductsAPIUrl + $"/api/products/{id}",
                AccessToken = ""
            }
                );
        }

        public async Task<T> UpdateProduct<T>(ProductDto productDto)
        {
            var link = "https://localhost:7055/api/prodcuts";
            return await SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = link,
               // Url = SD.ProductsAPIUrl + "/api/products/",
                AccessToken = ""
            }
                );
        }
    }
}
