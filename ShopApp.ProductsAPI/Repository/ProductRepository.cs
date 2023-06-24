using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.ProductsAPI.Data;
using ShopApp.ProductsAPI.Models;
using ShopApp.ProductsAPI.Models.Dtos;

namespace ShopApp.ProductsAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _productsDbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ProductsDbContext productsDbContext,IMapper mapper) 
        {
            _productsDbContext = productsDbContext;
            _mapper = mapper;
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _productsDbContext.Products
                    .FirstOrDefaultAsync(p=>p.ProductId==productId);
                if (product==null) return false;
                _productsDbContext.Products .Remove(product);
                await _productsDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product =await _productsDbContext.Products
                .Where(p=>p.ProductId==productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _productsDbContext.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> UpsertProduct(ProductDto productDto)
        {
            var product = _mapper .Map<ProductDto, Product>(productDto);
            if (product.ProductId > 0)
            {
                _productsDbContext.Products.Update(product);
            }
            else
            {
                _productsDbContext.Products.Add(product);
            }
            await _productsDbContext.SaveChangesAsync();
            return _mapper.Map<Product,ProductDto>(product);
            
        }
    }
}
