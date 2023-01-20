using ePizza.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Repositories.Models;

namespace ePizza.Repositories.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        IEnumerable<Order> GetUserOrders(int UserId);
        OrderModel GetOrderDetails(string id);
        PagingListModel<OrderModel> GetOrderList(int page, int pageSize);

    }
}
