using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using ePizza.Data.Concrete.EntityFramework.Contexts;

namespace ePizza.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private ePizzaContext _ePizzaContext
        {
            get
            {
                return _ePizzaContext as ePizzaContext;
            }
        }
        public OrderRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return _ePizzaContext.Orders
               .Include(o => o.OrderItems)
               .Where(x => x.UserId == UserId).ToList();
        }

        public OrderModel GetOrderDetails(string orderId)
        {
            var model = (from order in _ePizzaContext.Orders
                         where order.Id == orderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             CreatedDate = order.CreatedDate,
                             Products = (from orderItem in _ePizzaContext.OrderItems
                                      join product in _ePizzaContext.Products
                                      on orderItem.ProductId equals product.Id
                                      where orderItem.OrderId == orderId
                                      select new ProductModel()
                                      {
                                          Id = orderItem.Id,
                                          Name = product.Name,
                                          Description = product.Description,
                                          ImageUrl = product.ImageUrl,
                                          Quantity = orderItem.Quantity,
                                          ProductId = product.Id,
                                          UnitPrice = orderItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int pageSize)
        {
            var pagingModel = new PagingListModel<OrderModel>();
            var data = (from order in _ePizzaContext.Orders
                        join payment in _ePizzaContext.PaymentDetails
                        on order.PaymentId equals payment.Id
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            PaymentId = payment.Id,
                            CreatedDate = order.CreatedDate,
                            GrandTotal = payment.GrandTotal,
                            Locality = order.Locality
                        });

            int itemCounts = data.Count();
            var orders = data.Skip((page - 1) * pageSize).Take(pageSize);

            var pagedListData = new StaticPagedList<OrderModel>(orders, page, pageSize, itemCounts);

            pagingModel.Data = pagedListData;
            pagingModel.Page = page;
            pagingModel.PageSize = pageSize;
            pagingModel.TotalRows = itemCounts;
            return pagingModel;
        }
    }
}
