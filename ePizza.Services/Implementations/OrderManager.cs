using ePizza.Entities.Concrete;
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
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        public OrderManager(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }


        public OrderModel GetOrderDetails(string OrderId)
        {
            var model = _orderRepo.GetOrderDetails(OrderId);
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

        public PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10)
        {
            return _orderRepo.GetOrderList(page, pageSize);
        }

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return _orderRepo.GetUserOrders(UserId);
        }

        public async Task<int> PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address)
        {
            Order order = new Order
            {
                PaymentId = paymentId,
                UserId = userId,

                CreatedDate = DateTime.Now,
                Id = orderId,
                Street = address.Street,
                Locality = address.Locality,
                City = address.City,
                ZipCode = address.ZipCode,
                PhoneNumber = address.PhoneNumber
            };

            foreach (var item in cart.Products)
            {
                OrderItem orderItem = new OrderItem(item.ProductId, item.UnitPrice, item.Quantity, item.Total);
                order.OrderItems.Add(orderItem);
            }

            await  _orderRepo.AddAsync(order);
            return await _orderRepo.SaveAsync();
        }

    }
}
