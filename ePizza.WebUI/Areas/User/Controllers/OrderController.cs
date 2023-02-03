using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.WebUI.Interfaces;
using ePizza.WebUI.Areas.User.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.WebUI.Areas.User.Controllers
{
    public class OrderController : BaseController
    {
        readonly IOrderService _orderService;
        public OrderController(IOrderService orderService, IUserAccessor userAccessor) : base(userAccessor)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(CurrentUser.Id);
            if (orders!=null)
                return View(orders);
            else
                return View();
          
        }

        [Route("~/User/Order/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel Order = _orderService.GetOrderDetails(OrderId);
            return View(Order);
        }
    }
}
