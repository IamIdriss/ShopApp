using AutoMapper;
using ShopApp.CouponsAPI.Models;
using ShopApp.CouponsAPI.Models.Dtos;

namespace ShopApp.CouponsAPI.Repository
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<Coupon,CouponDto>().ReverseMap();
        }
    }
}
