﻿using ShopApp.ShoppingCartAPI.Models.Dto;
using ShopApp.ShoppingCartAPI.Models.Dtos;

namespace ShopApp.ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserId(string userId);
        Task<CartDto> UpsertCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> UpdateCount(CountDetailsDto countDetailsDto);
        Task<bool> ClearCart(string userId);
    }
}
