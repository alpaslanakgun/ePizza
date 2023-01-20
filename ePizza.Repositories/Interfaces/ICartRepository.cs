using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;

namespace ePizza.Repositories.Interfaces
{
    public interface ICartRepository:IRepository<Cart>
    {

        Cart GetCart(Guid CartId);

        CartModel GetCartDetails(Guid CartId);
        int DeleteItem(Guid cartId, int itemId);
        int UpdateQuantity(Guid cartId, int itemId, int Quantity);
        int UpdateCart(Guid cartId, int userId);
    }
}
