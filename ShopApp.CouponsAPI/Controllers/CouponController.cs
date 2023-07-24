using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.CouponsAPI.Models.Dtos;
using ShopApp.CouponsAPI.Repository;

namespace ShopApp.CouponsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly CouponResponseDto _response;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            _response = new CouponResponseDto();
        }

        [HttpGet("{code}")]
        public async Task<CouponResponseDto> GetCoupon(string code)
        {
            try
            {
                var couponDto = await _couponRepository.GetCouponByCode(code);
                _response.Result = couponDto;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    e.ToString()
                };
            }
            return _response;
        }
    }
}
