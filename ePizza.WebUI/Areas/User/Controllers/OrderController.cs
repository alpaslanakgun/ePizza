using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.WebUI.Interfaces;
using ePizzaHub.WebUI.Areas.User.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.WebUI.Area.User.Controllers
{
    public class OrderController : BaseController
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService, IUserAccessor userAccessor) : base(userAccessor)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(CurrentUser.Id);
            return View(orders);
        }

        [Route("~/User/Order/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel Order = _orderService.GetOrderDetails(OrderId);
            return View(Order);
        }
    }
}
