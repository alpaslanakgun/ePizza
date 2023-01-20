using ePizza.Entities;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.WebUI.Helpers;
using ePizza.WebUI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;

namespace ePizzaHub.WebUI.Controllers
{
    public class CartController : BaseController
    {
        ICartService _cartService;
        Guid CartId
        {
            get
            {
                Guid Id;
                string CId = Request.Cookies["CId"];
                if (string.IsNullOrEmpty(CId))
                {
                    Id = Guid.NewGuid();
                    Response.Cookies.Append("CId", Id.ToString());
                }
                else
                {
                    Id = Guid.Parse(CId);
                }
                return Id;
            }
        }
        public CartController(ICartService cartService, IUserAccessor userAccessor) : base(userAccessor)
        {
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            CartModel cart = _cartService.GetCartDetails(CartId);
            if (CurrentUser != null && cart != null)
            {
                TempData.Set("Cart", cart);
                _cartService.UpdateCart(cart.Id, CurrentUser.Id);
            }
            return View(cart);
        }

        [Route("Cart/AddToCart/{ItemId}/{UnitPrice}/{Quantity}")]
        public IActionResult AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            int UserId = CurrentUser != null ? CurrentUser.Id : 0;

            if (ItemId > 0 && Quantity > 0)
            {
                Cart cart = _cartService.AddItem(UserId, CartId, ItemId, UnitPrice, Quantity);
                var data = JsonSerializer.Serialize(cart);
                return Json(data);
            }
            else
            {
                return Json("");
            }
        }

        public IActionResult DeleteItem(int Id)
        {
            int count = _cartService.DeleteItem(CartId, Id);
            return Json(count);
        }

        [Route("Cart/UpdateQuantity/{Id}/{Quantity}")]
        public IActionResult UpdateQuantity(int Id, int Quantity)
        {
            int count = _cartService.UpdateQuantity(CartId, Id, Quantity);
            return Json(count);
        }

        public IActionResult GetCartCount()
        {
            int count = _cartService.GetCartCount(CartId);
            return Json(count);
        }

        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Address address)
        {
            TempData.Set("Address", address);
            return RedirectToAction("Index", "Payment");
        }
    }
}
