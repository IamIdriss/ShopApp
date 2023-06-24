using ShopApp.ProductsAPI.Models.Dtos;

namespace ShopApp.ProductsAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> GetProductById(int productId);

        Task<ProductDto> UpsertProduct(ProductDto productDto);

        Task<bool> DeleteProduct(int productId);
    }
}
