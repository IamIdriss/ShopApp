using AutoMapper;
using ShopApp.ProductsAPI.Models;
using ShopApp.ProductsAPI.Models.Dtos;

namespace ShopApp.ProductsAPI.Repository
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
        CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}
