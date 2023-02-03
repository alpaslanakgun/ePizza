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
        private ePizzaContext ePizzaContext
        {
            get
            {
                return _context as ePizzaContext;
            }
        }

        public CartRepository(DbContext context) : base(context)
        {
        }

        public Cart GetCart(Guid cartId)
        {
            try
            {
                var x = cartId;
                
                var result = ePizzaContext.Carts.Include("Products")
                    .Where(x => x.Id ==cartId && x.IsActive == true).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                return null;
            }
   
        
        }
          

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in ePizzaContext.Carts
                where cart.Id == CartId && cart.IsActive == true
                select new CartModel
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CreatedDate = cart.CreatedDate,
                    Products = (from cartItem in ePizzaContext.CartItems
                        join item in ePizzaContext.Products
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
            var item = ePizzaContext.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.Id == itemId);
            if (item != null)
            {
                ePizzaContext.CartItems.Remove(item);
                return ePizzaContext.SaveChanges();
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
                for (int i = 0; i < cart.Products.Count; i++)
                {
                    if (cart.Products[i].Id == itemId)
                    {
                        flag = true;
                        //for minus quantity
                        if (Quantity < 0 && cart.Products[i].Quantity > 1)
                            cart.Products[i].Quantity += (Quantity);
                        else if (Quantity > 0)
                            cart.Products[i].Quantity += (Quantity);
                        break;
                    }
                }
                if (flag)
                    return ePizzaContext.SaveChanges();
            }
            return 0;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return ePizzaContext.SaveChanges();
        }

      
    }
}
