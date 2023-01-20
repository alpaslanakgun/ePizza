using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.WebUI.Area.Admin.Controllers
{
    public class DashboardController : Controller
    {
        IOrderService _orderService;
        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index(int page = 1, int pageSize = 2)
        {
            var orders = _orderService.GetOrderList(page, pageSize);
            return View(orders);
        }

        [Route("~/Admin/Dashboard/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel Order = _orderService.GetOrderDetails(OrderId);
            return View(Order);
        }
    }
}
