using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public interface IProductService
    {
        Task<T> GetAllProducts<T>();
        Task<T> GetProduct<T>(int id);
        Task<T> CreateProduct<T>(ProductDto productDto);
        Task<T> UpdateProduct<T>(ProductDto productDto);

        Task<T> DeleteProduct<T>(int id);
    }
}
