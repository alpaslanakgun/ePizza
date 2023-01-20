﻿using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implementations
{
    public class CartManager : ICartService
    {

        private readonly ICartRepository _cartRepo;
        private readonly IRepository<CartItem> _cartItem;
        public CartManager(ICartRepository cartRepo, IRepository<CartItem> cartItem)
        {
            _cartRepo = cartRepo;
            _cartItem = cartItem;
        }

        public Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity)
        {
            try
            {
                Cart cart = _cartRepo.GetCart(CartId);
                if (cart == null)
                {
                    cart = new Cart();
                    CartItem item = new CartItem(ItemId, Quantity, UnitPrice);
                    cart.Id = CartId;
                    cart.UserId = UserId;
                    cart.CreatedDate = DateTime.Now;

                    item.CartId = cart.Id;
                    cart.CartItems.Add(item);
                    _cartRepo.AddAsync(cart);
                    _cartRepo.SaveAsync();
                }
                else
                {
                

                    CartItem item = cart.CartItems.FirstOrDefault(p => p.ProductId == ItemId);

                    if (item != null)
                    {
                        item.Quantity += Quantity;
                        _cartItem.UpdateAsync(item);
                        _cartItem.SaveAsync();
                    }
                    else
                    {
                        item = new CartItem(ItemId, Quantity, UnitPrice);
                        item.CartId = cart.Id;
                        cart.CartItems.Add(item);

                        _cartItem.UpdateAsync(item);
                        _cartItem.SaveAsync();
                    }
                }
                return cart;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeleteItem(Guid cartId, int ItemId)
        {
            return _cartRepo.DeleteItem(cartId, ItemId);
        }

        public int GetCartCount(Guid cartId)
        {
            var cart = _cartRepo.GetCart(cartId);
            return cart != null ? cart.CartItems.Count() : 0;
        }

        public  CartModel GetCartDetails(Guid cartId)
        {
            var model = _cartRepo.GetCartDetails(cartId);
            if (model != null && model.Products.Count > 0)
            {
                decimal subTotal = 0;
                foreach (var item in model.Products)
                {
                    item.Total = item.UnitPrice * item.Quantity;
                    subTotal += item.Total;
                }
                model.Total = subTotal;
                //5% tax
                model.Tax = Math.Round((model.Total * 5) / 100, 2);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
            return _cartRepo.UpdateCart(CartId, UserId);
        }

        public int UpdateQuantity(Guid cartId, int id, int quantity)
        {
            return _cartRepo.UpdateQuantity(cartId, id, quantity);
        }
    }
}
