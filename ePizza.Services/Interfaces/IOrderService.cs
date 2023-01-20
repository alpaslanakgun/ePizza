using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Interfaces
{
    public interface IOrderService
    {
        OrderModel GetOrderDetails(string orderId);
        IEnumerable<Order> GetUserOrders(int userId);
        Task<int> PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address);
        PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10);
    }
}
