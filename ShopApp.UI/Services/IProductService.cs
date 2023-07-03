using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public interface IProductService
    {
        Task<T> GetAllProducts<T>(string token);
        Task<T> GetProduct<T>(int id, string token);
        Task<T> CreateProduct<T>(ProductDto productDto, string token);
        Task<T> UpdateProduct<T>(ProductDto productDto, string token);

        Task<T> DeleteProduct<T>(int id, string token);
    }
}
