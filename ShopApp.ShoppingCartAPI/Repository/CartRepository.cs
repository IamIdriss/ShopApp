using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopApp.ShoppingCartAPI.Models;
using ShopApp.ShoppingCartAPI.Models.Dto;
using ShopApp.ShoppingCartAPI.Models.Dtos;
using ShopApp.ShoppingCartAPI.ShoppingCartData;
using System.Xml.Linq;

namespace ShopApp.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ShoppingCartDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(ShoppingCartDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        

        public async Task<CartDto> UpsertCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);
            try
            {

                //check if product exists in database, if not create it!
                var prodInDb = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.FirstOrDefault()
                    .ProductId);
                if (prodInDb == null)
                {
                    _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                    await _context.SaveChangesAsync();
                }


                //check if header is null
                var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    _context.CartHeaders.Add(cart.CartHeader);
                    await _context.SaveChangesAsync();
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;

                    var details = cart.CartDetails.FirstOrDefault();
                    try
                    {
                        cart.CartDetails.FirstOrDefault().Product = null;
                        _context.CartDetails.Add(details);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    //if header is not null
                    //check if details has same product
                    var cartDetailsFromDb = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if (cartDetailsFromDb == null)
                    {
                        //create details
                        cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        try
                        {
                            var details = cart.CartDetails.FirstOrDefault();
                            _context.Entry(details).State = EntityState.Added;
                            _context.CartDetails.Add(details);
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //update the count / cart details
                        cart.CartDetails.FirstOrDefault().Product = null;
                        cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                        cart.CartDetails.FirstOrDefault().CartDetailId = cartDetailsFromDb.CartDetailId;
                        cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsFromDb.CartHeaderId;

                        var details = cart.CartDetails.FirstOrDefault();
                        _context.Entry(details).State = EntityState.Modified;
                        _context.CartDetails.Update(details);

                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeaders
                .FirstOrDefaultAsync(ch => ch.UserId == userId)
            };

            cart.CartDetails = _context.CartDetails
                .Where(cd => cd.CartHeaderId == cart.CartHeader.CartHeaderId)
                .Include(cd => cd.Product);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _context.CartDetails
                    .FirstOrDefaultAsync(u => u.CartDetailId == cartDetailsId);

                int totlCountOfCartItems = _context.CartDetails
                    .Where(cd => cd.CartHeaderId == cartDetails.CartHeaderId)
                    .Count();

                _context.CartDetails.Remove(cartDetails);

                if (totlCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(ch =>
                        ch.CartHeaderId == cartDetails.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCount(CountDetailsDto countDetails)
        {
            CartDetails cartDetails = await _context.CartDetails
                    .FirstOrDefaultAsync(cd => cd.CartDetailId == countDetails.CartDetailsId);
            if (cartDetails != null)
            {
                if (countDetails.Action == "decrement")
                {
                    if (cartDetails.Count > 1)
                    {
                        cartDetails.Count -= countDetails.Amount;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await RemoveFromCart(countDetails.CartDetailsId);
                    }
                }
                if (countDetails.Action == "increment")
                {
                    cartDetails.Count += countDetails.Amount;
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _context.CartHeaders
                .FirstOrDefaultAsync(ch => ch.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                _context.CartDetails.RemoveRange(_context.CartDetails
                    .Where(cd => cd.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _context.CartHeaders.Remove(cartHeaderFromDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
