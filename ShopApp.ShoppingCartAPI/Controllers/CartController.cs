using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.ProductsAPI.Models.Dtos;
using ShopApp.ShoppingCartAPI.Messages;
using ShopApp.ShoppingCartAPI.Models.Dto;
using ShopApp.ShoppingCartAPI.Models.Dtos;
using ShopApp.ShoppingCartAPI.Repository;

namespace ShopApp.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ShoppingCartResponseDto _response;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _response = new ShoppingCartResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;
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

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                var result = await _cartRepository.UpsertCart(cartDto);
                _response.Result = result;
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

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                var result = await _cartRepository.UpsertCart(cartDto);
                _response.Result = result;

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

        [HttpPost("RemoveFromCart")]
        public async Task<object> RemoveFromCart([FromBody] int cartDetailsId)
        {
            try
            {
                var isSuccess = await _cartRepository.RemoveFromCart(cartDetailsId);
                _response.Result = isSuccess;

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

        [HttpPost("UpdateCount")]
        public async Task<object> UpdateCount(CountDetailsDto count)
        {
            try
            {
                var isSuccess = await _cartRepository.UpdateCount(count);
                _response.Result = isSuccess;

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

        [HttpPost("ClearCart")]
        public async Task<object> ClearCart([FromBody] string userId)
        {
            try
            {
                var isSuccess = await _cartRepository.ClearCart(userId);
                _response.Result = isSuccess;

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

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var isSuccess = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId,
                    cartDto.CartHeader.CouponCode);
                _response.Result = isSuccess;

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

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                var isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;

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

        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutMessageDto messageDto)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserId(messageDto.UserId);
                if (cartDto == null)
                {
                    return BadRequest();
                }
              
                messageDto.CartDetails = cartDto.CartDetails;
                //logic code to add message to process order via azure service bus
                

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
