using System;
using System.Collections.Generic;
using System.Linq;
using ePizza.Data.Concrete.EntityFramework.Contexts;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Repositories.Implementations
{
    public class CartRepository:Repository<Cart>,ICartRepository
    {
        private ePizzaContext _ePizzaContext
        {
            get
            {
                return _ePizzaContext as ePizzaContext;
            }
        } 
        public CartRepository(DbContext context) : base(context)
        {
        }

        public Cart GetCart(Guid CartId)
        {
            return _ePizzaContext.Carts.Include("Products")
                .FirstOrDefault(x => x.Id == CartId && x.IsActive == true);
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in _ePizzaContext.Carts
                where cart.Id == CartId && cart.IsActive == true
                select new CartModel
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CreatedDate = cart.CreatedDate,
                    Products = (from cartItem in _ePizzaContext.CartItems
                        join item in _ePizzaContext.Products
                            on cartItem.ProductId equals item.Id
                        where cartItem.CartId == CartId
                        select new ProductModel()
                        {
                            Id = cartItem.Id,
                            Name = item.Name,
                            Description = item.Description,
                            ImageUrl = item.ImageUrl,
                            Quantity = cartItem.Quantity,
                            ProductId = item.Id,
                            UnitPrice = cartItem.UnitPrice
                        }).ToList()
                }).FirstOrDefault();
            return model;
        }

        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = _ePizzaContext.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.Id == itemId);
            if (item != null)
            {
                _ePizzaContext.CartItems.Remove(item);
                return _ePizzaContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int UpdateQuantity(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId);
            if (cart != null)
            {
                for (int i = 0; i < cart.CartItems.Count; i++)
                {
                    if (cart.CartItems[i].Id == itemId)
                    {
                        flag = true;
                        //for minus quantity
                        if (Quantity < 0 && cart.CartItems[i].Quantity > 1)
                            cart.CartItems[i].Quantity += (Quantity);
                        else if (Quantity > 0)
                            cart.CartItems[i].Quantity += (Quantity);
                        break;
                    }
                }
                if (flag)
                    return _ePizzaContext.SaveChanges();
            }
            return 0;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return _ePizzaContext.SaveChanges();
        }

      
    }
}
