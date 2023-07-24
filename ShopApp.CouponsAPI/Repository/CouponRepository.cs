using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.CouponsAPI.CouponData;
using ShopApp.CouponsAPI.Models.Dtos;

namespace ShopApp.CouponsAPI.Repository
{
    public class CouponRepository : ICouponRepository

    {
        private readonly CouponDbContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(CouponDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            var CouponFromDb = await _context.Coupons
                .FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponDto>(CouponFromDb);
        }
    }
}
